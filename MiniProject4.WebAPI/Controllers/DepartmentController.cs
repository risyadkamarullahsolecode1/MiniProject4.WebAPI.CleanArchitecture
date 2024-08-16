using Microsoft.AspNetCore.Mvc;
using MiniProject4.Application.Interfaces;
using MiniProject4.Application.Services;
using MiniProject4.Domain.Entities;
using MiniProject4.Domain.Interfaces;

namespace MiniProject4.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public readonly IDepartmentRepository _departemntRepository;
        public readonly IDepartmentServices _departmentServices;
        public DepartmentController(IDepartmentRepository departemntRepository, IDepartmentServices departemntServices)
        {
            _departemntRepository = departemntRepository;
            _departmentServices = departemntServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllDepartment()
        {
            return Ok(await _departemntRepository.GetAllDepartments());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetDepartmentById(int id)
        {
            var employee = await _departemntRepository.GetDepartmentById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddDepartment(Department department)
        {
            var createdDepartment = await _departemntRepository.AddDepartment(department);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Deptno }, createdDepartment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            if (id != department.Deptno)
            {
                return BadRequest();
            }

            var updatedDepartment = await _departemntRepository.UpdateDepartment(id, department);
            if (updatedDepartment == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var deleted = await _departemntRepository.DeleteDepartment(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("more-10-employees")]
        public async Task<ActionResult<IEnumerable<object>>> GetDepartmentsWithMoreThan10Employees()
        {
            return Ok(await _departmentServices.GetDepartmentsWithMoreThan10Employees());
        }

        [HttpGet("it-Department")]
        public async Task<ActionResult<IEnumerable<object>>> GetEmployeeDetailsByDepartment()
        {
            return Ok(await _departmentServices.GetEmployeeDetailsByDepartment("IT"));
        }
    }
}
