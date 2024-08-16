﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiniProject4.Application.Interfaces;
using MiniProject4.Application.Services;
using MiniProject4.Domain.Entities;
using MiniProject4.Domain.Interfaces;
using MiniProject4.Infrastructure.Data.Repositories;

namespace MiniProject4.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
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
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeesAsync()
        {
            return Ok(await _employeeRepository.GetAllEmployees());
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            var createdEmployee = await _employeeRepository.AddEmployee(employee);
            return Ok(createdEmployee);
            //return CreatedAtAction(nameof(GetEmployeeByIdAsync), new { id = createdEmployee.Empno }, createdEmployee);
        }
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
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

        [HttpGet("brics")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesBrics()
        {
            return Ok(await _employeeService.GetEmployeesBrics());
        }

        [HttpGet("born-1980-1990")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeBornBetween1980And1990()
        {
            return Ok(await _employeeService.GetEmployeeBornBetween1980And1990());
        }

        [HttpGet("female-born-after1990")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetFemaleEmployeeBornAfter1990()
        {
            return Ok(await _employeeService.GetFemaleEmployeeBornAfter1990());
        }

        [HttpGet("female-managers")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetFemaleManagers()
        {
            return Ok(await _employeeService.GetFemaleManagers());
        }

        [HttpGet("non-managers")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetNonManagerEmployees()
        {
            return Ok(await _employeeService.GetNonManagerEmployees());
        }
        [HttpGet("it-department")]
        public async Task<IActionResult> GetITDepartmentEmployees()
        {
            var employees = await _employeeService.GetITDepartmentEmployeesAsync();
            return Ok(employees);
        }

        [HttpPost("validate-retirement")]
        public async Task<IActionResult> ValidateRetirement([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee data is required.");
            }

            bool shouldRetire = await _employeeService.ValidateRetirementAsync(employee);

            if (shouldRetire)
            {
                return Ok(new { Message = "The employee should retire.", Retire = true });
            }
            else
            {
                return Ok(new { Message = "The employee does not need to retire.", Retire = false });
            }
        }
        [HttpGet("can-add-to-it")]
        public async Task<IActionResult> CanAddToITDepartment()
        {
            bool canAdd = await _employeeService.CanAddToITDepartmentAsync();

            if (canAdd)
            {
                return Ok(new { Message = "You can add more employees to the IT department." });
            }
            else
            {
                return Ok(new { Message = "The IT department has reached its maximum capacity." });
            }
        }
    }
}
