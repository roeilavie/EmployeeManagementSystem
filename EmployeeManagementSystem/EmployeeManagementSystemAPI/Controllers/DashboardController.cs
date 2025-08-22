using EmployeeManagementSystemAPI.Models;
using EmployeeManagementSystemAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public DashboardController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        // GET: api/dashboard/total-employees
        [HttpGet("total-employees")]
        public async Task<ActionResult<int>> GetTotalEmployees()
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetAllAsync();
            return Ok(employees.Count());
        }

        // GET: api/dashboard/employees-by-department/1
        [HttpGet("employees-by-department/{departmentId}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByDepartment(int departmentId)
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetAllAsync();
            Department? department = await _departmentRepository.GetByIdAsync(departmentId);

            if (department == null)
                return NotFound();

            IEnumerable<Employee> result = employees.Where(e => e.DepartmentId == departmentId);

            return Ok(result);
        }

        // GET: api/dashboard/recent-hires
        [HttpGet("recent-hires")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetRecentHires()
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetAllAsync();
            IEnumerable<Employee> recentEmployees = employees
                .Where(e => e.HireDate >= DateTime.UtcNow.AddDays(-30))
                .OrderByDescending(e => e.HireDate);

            return Ok(recentEmployees);
        }

        // GET: api/dashboard/search?query=John
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployees([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query cannot be empty.");

            IEnumerable<Employee> employees = await _employeeRepository.GetAllAsync();
            IEnumerable<Department> departments = await _departmentRepository.GetAllAsync();

            IEnumerable<Employee> result = employees.Where(employee =>
                employee.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                employee.LastName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                departments.Any(depart => depart.Id == employee.DepartmentId && depart.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
            );

            return Ok(result);
        }
    }
}
