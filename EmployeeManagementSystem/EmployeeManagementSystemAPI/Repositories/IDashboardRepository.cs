using EmployeeManagementSystemAPI.Models;

namespace EmployeeManagementSystemAPI.Repositories
{
    public interface IDashboardRepository
    {
        Task<int> GetTotalEmployeesAsync();
        Task<IEnumerable<Employee>?> GetEmployeesByDepartmentAsync(int departmentId);
        Task<IEnumerable<Employee>> GetRecentHiresAsync();
        Task<IEnumerable<Employee>?> SearchEmployeesAsync(string query);
    }
}
