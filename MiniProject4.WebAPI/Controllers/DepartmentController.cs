using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiniProject4.Application.Interfaces;
using MiniProject4.Application.Services;
using MiniProject4.Domain.Entities;
using MiniProject4.Domain.Interfaces;

namespace MiniProject4.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
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
        ///     GET /api/v1/Department
        ///     
        ///     OR
        ///     
        ///     GET /api/v1/Department/{id}
        ///    
        ///     OR
        ///     
        ///     DELETE /api/v1/Department/{id}
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllDepartment()
        {
            return Ok(await _departemntRepository.GetAllDepartments());
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
        ///     GET /api/v1/Department
        ///     
        ///     OR
        ///     
        ///     GET /api/v1/Department/{id}
        ///    
        ///     OR
        ///     
        ///     DELETE /api/v1/Department/{id}
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Employee>> AddDepartment(Department department)
        {
            var createdDepartment = await _departemntRepository.AddDepartment(department);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Deptno }, createdDepartment);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
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
        ///     GET /api/v1/Department
        ///     
        ///     OR
        ///     
        ///     GET /api/v1/Department/{id}
        ///    
        ///     OR
        ///     
        ///     DELETE /api/v1/Department/{id}
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<object>>> GetDepartmentsWithMoreThan10Employees()
        {
            return Ok(await _departmentServices.GetDepartmentsWithMoreThan10Employees());
        }

        [HttpGet("it-Department")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<object>>> GetEmployeeDetailsByDepartment()
        {
            return Ok(await _departmentServices.GetEmployeeDetailsByDepartment("IT"));
        }
    }
}
