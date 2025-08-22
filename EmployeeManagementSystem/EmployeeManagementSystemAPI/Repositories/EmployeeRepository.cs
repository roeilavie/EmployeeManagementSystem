using EmployeeManagementSystemAPI.Models;
using System.Text.Json;

namespace EmployeeManagementSystemAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IJsonDataRepository _jsonDataRepository;

        public EmployeeRepository(IJsonDataRepository jsonDataRepository)
        {
            _jsonDataRepository = jsonDataRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            JsonData data = await _jsonDataRepository.GetDataAsync();
            return data.Employees;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            IEnumerable<Employee> employees = await GetAllAsync(); // cannot use FirstOrDefault directly like DB
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            JsonData data = await _jsonDataRepository.GetDataAsync();
            employee.Id = data.Employees.Any() ? data.Employees.Max(e => e.Id) + 1 : 1; // find the max id of existing employee

            if (!data.Departments.Any(d => d.Id == employee.DepartmentId))
                throw new Exception("Invalid DepartmentId. Department does not exist.");

            data.Employees.Add(employee);
            await _jsonDataRepository.SaveDataAsync(data);

            return employee;
        }

        public async Task<Employee?> UpdateAsync(Employee employee)
        {
            JsonData data = await _jsonDataRepository.GetDataAsync();
            Employee? existingEmployee = data.Employees.FirstOrDefault(e => e.Id == employee.Id);

            if (existingEmployee == null)
                throw new Exception("Employee does not exist.");

            if (!data.Departments.Any(d => d.Id == employee.DepartmentId))
                throw new Exception("Invalid DepartmentId. Department does not exist.");

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.HireDate = employee.HireDate;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.DepartmentId = employee.DepartmentId;

            await _jsonDataRepository.SaveDataAsync(data);
            return existingEmployee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            JsonData data = await _jsonDataRepository.GetDataAsync();
            Employee? employee = data.Employees.FirstOrDefault(e => e.Id == id);

            if (employee == null) return false;

            data.Employees.Remove(employee);
            await _jsonDataRepository.SaveDataAsync(data);

            return true;
        }

        public async Task<object> GetPagedAsync(int pageNumber, int pageSize, string? sortBy, string? sortDirection)
        {
            IEnumerable<Employee> employees = await GetAllAsync();

            employees = (sortBy?.ToLower(), sortDirection?.ToLower()) switch
            {
                ("firstname", "desc") => employees.OrderByDescending(e => e.FirstName),
                ("firstname", _) => employees.OrderBy(e => e.FirstName),

                ("lastname", "desc") => employees.OrderByDescending(e => e.LastName),
                ("lastname", _) => employees.OrderBy(e => e.LastName),

                ("email", "desc") => employees.OrderByDescending(e => e.Email),
                ("email", _) => employees.OrderBy(e => e.Email),

                ("hiredate", "desc") => employees.OrderByDescending(e => e.HireDate),
                ("hiredate", _) => employees.OrderBy(e => e.HireDate),

                ("salary", "desc") => employees.OrderByDescending(e => e.Salary),
                ("salary", _) => employees.OrderBy(e => e.Salary),

                _ => sortDirection?.ToLower() == "desc"
                    ? employees.OrderByDescending(e => e.Id)
                    : employees.OrderBy(e => e.Id)
            };

            int totalItems = employees.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return new 
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = employees.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
            };
        }
    }
}
