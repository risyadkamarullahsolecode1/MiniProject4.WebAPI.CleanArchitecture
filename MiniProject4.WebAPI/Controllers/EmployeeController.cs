using Microsoft.AspNetCore.Mvc;
using MiniProject4.Application.Interfaces;
using MiniProject4.Application.Services;
using MiniProject4.Domain.Entities;
using MiniProject4.Domain.Interfaces;
using MiniProject4.Infrastructure.Data.Repositories;

namespace MiniProject4.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeesAsync()
        {
            return Ok(await _employeeRepository.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            var createdEmployee = await _employeeRepository.AddEmployee(employee);
            return Ok(createdEmployee);
            //return CreatedAtAction(nameof(GetEmployeeByIdAsync), new { id = createdEmployee.Empno }, createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Empno)
            {
                return BadRequest();
            }

            var updatedEmployee = await _employeeRepository.UpdateEmployee(employee);
            if (updatedEmployee == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _employeeRepository.DeleteEmployee(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("Hire-Employee")]
        public async Task<IActionResult> HireEmployee([FromBody] Employee employee)
        {
            try
            {
                var hireEmployee = await _employeeService.HireEmployee(employee);
                return Ok(hireEmployee);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("EmployeeToITDepartment")]
        public async Task<IActionResult> EmployeeToITDepartment(int empNo, int deptNo, Employee employee)
        {
            try
            {
                await _employeeService.EmployeeToITDepartment(empNo, deptNo, employee);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("brics")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesBrics()
        {
            return Ok(await _employeeService.GetEmployeesBrics());
        }
        [HttpGet(("born-1980-1990"))]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeBornBetween1980And1990()
        {
            return Ok(await _employeeService.GetEmployeeBornBetween1980And1990());
        }
        [HttpGet("female-born-after1990")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetFemaleEmployeeBornAfter1990()
        {
            return Ok(await _employeeService.GetFemaleEmployeeBornAfter1990());
        }
        [HttpGet("female-managers")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetFemaleManagers()
        {
            return Ok(await _employeeService.GetFemaleManagers());
        }
        [HttpGet("non-managers")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetNonManagerEmployees()
        {
            return Ok(await _employeeService.GetNonManagerEmployees());
        }
    }
}
