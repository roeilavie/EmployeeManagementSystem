using EmployeeManagementSystemAPI.Models;

namespace EmployeeManagementSystemAPI.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public DashboardRepository(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<int> GetTotalEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetAllAsync();
            return employees.Count();
        }

        public async Task<IEnumerable<Employee>?> GetEmployeesByDepartmentAsync(int departmentId)
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetAllAsync();
            Department? department = await _departmentRepository.GetByIdAsync(departmentId);

            if (department == null)
                return null;

            return employees.Where(e => e.DepartmentId == departmentId);
        }

        public async Task<IEnumerable<Employee>> GetRecentHiresAsync()
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetAllAsync();
            return employees.Where(e => e.HireDate >= DateTime.UtcNow.AddDays(-30)).OrderByDescending(e => e.HireDate);
        }

        public async Task<IEnumerable<Employee>?> SearchEmployeesAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return null;

            IEnumerable<Employee> employees = await _employeeRepository.GetAllAsync();
            IEnumerable<Department> departments = await _departmentRepository.GetAllAsync();

            return employees.Where(employee =>
                employee.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                employee.LastName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                departments.Any(depart => depart.Id == employee.DepartmentId && depart.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
            );
        }
    }
}
