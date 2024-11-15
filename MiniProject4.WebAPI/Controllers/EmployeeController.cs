using Asp.Versioning;
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
        /// <summary>
        /// You get, get by id or delete here
        /// </summary>

        /// <remarks>
        /// All the parameters in the request body can be null. 
        ///
        ///  You can search by using any of the parameters in the request.
        ///  
        ///  NOTE: You can only search by one parameter at a time
        ///  
        /// Sample request:
        ///
        ///     GET /api/v1/Employee
        ///     
        ///     OR
        ///     
        ///     GET /api/v1/Employee/{id}
        ///    
        ///     OR
        ///     
        ///     DELETE /api/v1/Employee/{id}
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeesAsync()
        {
            return Ok(await _employeeRepository.GetAllEmployees());
        }
        /// <summary>
        /// You get, get by id or delete here
        /// </summary>

        /// <remarks>
        /// All the parameters in the request body can be null. 
        ///
        ///  You can search by using any of the parameters in the request.
        ///  
        ///  NOTE: You can only search by one parameter at a time
        ///  
        /// Sample request:
        ///
        ///     GET /api/v1/Employee
        ///     
        ///     OR
        ///     
        ///     GET /api/v1/Employee/{id}
        ///    
        ///     OR
        ///     
        ///     DELETE /api/v1/Employee/{id}
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
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
        /// <summary>
        /// You can add or update user here
        /// </summary>

        /// <remarks>
        /// All the parameters in the request body can be null. 
        ///
        ///  You can search by using any of the parameters in the request.
        ///  
        ///  NOTE: You can only search by one parameter at a time
        ///  
        /// Sample request:
        ///
        ///     POST /api/v1/Employee
        ///     {
        ///         "empno": 31,
        ///         "fname": "Anya",
        ///         "lname": "Vasily",
        ///         "address": "Moskowa, Russia",
        ///         "dob": "1975-08-09",
        ///         "sex": "Female",
        ///         "position": "Content Planner",
        ///         "deptno": 31, 
        ///         "departments": [],
        ///         "deptnoNavigation": null,
        ///         "worksons": []
        ///     }
        ///     OR
        ///     
        ///     PUT /api/v1/Employee/{id}
        ///     {
        ///         "empno": 31,
        ///         "fname": "Anya",
        ///         "lname": "Vasily",
        ///         "address": "Moskowa, Russia",
        ///         "dob": "1975-08-09",
        ///         "sex": "Female",
        ///         "position": "Content Planner",
        ///         "deptno": 31, 
        ///         "departments": [],
        ///         "deptnoNavigation": null,
        ///         "worksons": []
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            var createdEmployee = await _employeeRepository.AddEmployee(employee);
            return Ok(createdEmployee);
            //return CreatedAtAction(nameof(GetEmployeeByIdAsync), new { id = createdEmployee.Empno }, createdEmployee);
        }/// <summary>
         /// You can add or update user here
         /// </summary>

         /// <remarks>
         /// All the parameters in the request body can be null. 
         ///
         ///  You can search by using any of the parameters in the request.
         ///  
         ///  NOTE: You can only search by one parameter at a time
         ///  
         /// Sample request:
         ///
         ///     POST /api/v1/Employee
         ///     {
         ///         "empno": 31,
         ///         "fname": "Anya",
         ///         "lname": "Vasily",
         ///         "address": "Moskowa, Russia",
         ///         "dob": "1975-08-09",
         ///         "sex": "Female",
         ///         "position": "Content Planner",
         ///         "deptno": 31, 
         ///         "departments": [],
         ///         "deptnoNavigation": null,
         ///         "worksons": []
         ///     }
         ///     OR
         ///     
         ///     PUT /api/v1/Employee/{id}
         ///     {
         ///         "empno": 31,
         ///         "fname": "Anya",
         ///         "lname": "Vasily",
         ///         "address": "Moskowa, Russia",
         ///         "dob": "1975-08-09",
         ///         "sex": "Female",
         ///         "position": "Content Planner",
         ///         "deptno": 31, 
         ///         "departments": [],
         ///         "deptnoNavigation": null,
         ///         "worksons": []
         ///     }
         /// </remarks>
         /// <param name="request"></param>
         /// <returns> This endpoint returns a list of Accounts.</returns>
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
        }/// <summary>
         /// You get, get by id or delete here
         /// </summary>

         /// <remarks>
         /// All the parameters in the request body can be null. 
         ///
         ///  You can search by using any of the parameters in the request.
         ///  
         ///  NOTE: You can only search by one parameter at a time
         ///  
         /// Sample request:
         ///
         ///     GET /api/v1/Employee
         ///     
         ///     OR
         ///     
         ///     GET /api/v1/Employee/{id}
         ///    
         ///     OR
         ///     
         ///     DELETE /api/v1/Employee/{id}
         ///     
         /// </remarks>
         /// <param name="request"></param>
         /// <returns> This endpoint returns a list of Accounts.</returns>
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
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesBrics()
        {
            return Ok(await _employeeService.GetEmployeesBrics());
        }

        [HttpGet("born-1980-1990")]
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

        [HttpGet("{projNo}/projects")]
        public async Task<IActionResult> GetDepartmentAsync(int projNo)
        {
            var res = await _employeeRepository.GetEmployeesByProjectAsync(projNo);
            return Ok(res);
        }
    }
}
