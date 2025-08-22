using EmployeeManagementSystemAPI.Models;
using EmployeeManagementSystemAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeesController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        // GET: api/employees/paged?pageNumber={pageNumber}&pageSize={pageSize}
        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? sortBy = "Id",
            [FromQuery] string? sortDirection = "asc")
        {
            var result = await _employeeRepository.GetPagedAsync(pageNumber, pageSize, sortBy, sortDirection);
            return Ok(result);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            Employee? employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<ActionResult<Employee>> Create(Employee employee)
        {
            // Validate department exists
            Department? department = await _departmentRepository.GetByIdAsync(employee.DepartmentId);
            if (department == null)
                return BadRequest("Invalid DepartmentId. Department does not exist.");

            await _employeeRepository.AddAsync(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        // PUT: api/employees
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();

            Employee? existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null) return NotFound();

            Department? department = await _departmentRepository.GetByIdAsync(employee.DepartmentId);
            if (department == null)
                return BadRequest("Invalid DepartmentId. Department does not exist.");

            await _employeeRepository.UpdateAsync(employee);
            return NoContent();
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Employee? existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null) return NotFound();

            await _employeeRepository.DeleteAsync(id);
            return NoContent();
        }


    }
}
