using EmployeeManagementSystemAPI.Models;

namespace EmployeeManagementSystemAPI.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IJsonDataRepository _jsonDataRepository;

        public DepartmentRepository(IJsonDataRepository jsonDataRepository)
        {
            _jsonDataRepository = jsonDataRepository;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            JsonData data = await _jsonDataRepository.GetDataAsync();
            return data.Departments;
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            IEnumerable<Department> departments = await GetAllAsync(); // cannot use FirstOrDefault directly like DB
            return departments.FirstOrDefault(d => d.Id == id);
        }

        public async Task<Department> AddAsync(Department department)
        {
            JsonData data = await _jsonDataRepository.GetDataAsync();

            department.Id = data.Departments.Any() ? data.Departments.Max(d => d.Id) + 1 : 1; // find the max id of existing department

            data.Departments.Add(department);
            await _jsonDataRepository.SaveDataAsync(data);

            return department;
        }

        public async Task<Department?> UpdateAsync(Department department)
        {
            JsonData data = await _jsonDataRepository.GetDataAsync();
            Department? existingDepartment = data.Departments.FirstOrDefault(d => d.Id == department.Id);

            if (existingDepartment == null) return null;

            existingDepartment.Name = department.Name;

            await _jsonDataRepository.SaveDataAsync(data);
            return existingDepartment;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            JsonData data = await _jsonDataRepository.GetDataAsync();

            if (data.Employees.Any(e => e.DepartmentId == id)) // Prevent deletion if employees still belong to this department
                throw new Exception("Cannot delete department with employees.");

            Department? department = data.Departments.FirstOrDefault(d => d.Id == id);
            if (department == null) return false;

            data.Departments.Remove(department);
            await _jsonDataRepository.SaveDataAsync(data);

            return true;
        }
    }
}
