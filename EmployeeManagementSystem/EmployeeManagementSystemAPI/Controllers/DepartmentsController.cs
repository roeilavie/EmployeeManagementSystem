using EmployeeManagementSystemAPI.Models;
using EmployeeManagementSystemAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // GET: api/departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAll()
        {
            IEnumerable<Department> departments = await _departmentRepository.GetAllAsync();
            return Ok(departments);
        }

        // GET: api/departments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetById(int id)
        {
            Department? department = await _departmentRepository.GetByIdAsync(id);
            if (department == null) return NotFound();
            return Ok(department);
        }

        // POST: api/departments
        [HttpPost]
        public async Task<ActionResult<Department>> Create(Department department)
        {
            await _departmentRepository.AddAsync(department);
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        // PUT: api/departments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Department department)
        {
            if (id != department.Id) return BadRequest();

            Department? existingDepartment = await _departmentRepository.GetByIdAsync(department.Id);
            if (existingDepartment == null) return NotFound();

            await _departmentRepository.UpdateAsync(department);
            return NoContent();
        }

        // DELETE: api/departments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Department? existingDepartment = await _departmentRepository.GetByIdAsync(id);
            if (existingDepartment == null) return NotFound();

            try
            {
                await _departmentRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
