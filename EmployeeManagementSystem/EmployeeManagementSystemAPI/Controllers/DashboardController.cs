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
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        // GET: api/dashboard/total-employees
        [HttpGet("total-employees")]
        public async Task<ActionResult<int>> GetTotalEmployees()
        {
            return Ok(await _dashboardRepository.GetTotalEmployeesAsync());
        }

        // GET: api/dashboard/employees-by-department/1
        [HttpGet("employees-by-department/{departmentId}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByDepartment(int departmentId)
        {
            IEnumerable<Employee>? employees = await _dashboardRepository.GetEmployeesByDepartmentAsync(departmentId);

            if (employees == null)
                return NotFound();

            return Ok(employees);
        }

        // GET: api/dashboard/recent-hires
        [HttpGet("recent-hires")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetRecentHires()
        {
            return Ok(await _dashboardRepository.GetRecentHiresAsync());
        }

        // GET: api/dashboard/search?query=John
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployees([FromQuery] string query)
        {
            IEnumerable<Employee>? employees = await _dashboardRepository.SearchEmployeesAsync(query);

            if (employees == null)
                return BadRequest("Query cannot be empty.");

            return Ok(employees);
        }
    }
}
