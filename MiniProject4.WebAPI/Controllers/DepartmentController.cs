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
    public class DepartmentController : ControllerBase
    {
        public readonly IDepartmentRepository _departmentRepository;
        public readonly IDepartmentServices _departmentServices;
        public DepartmentController(IDepartmentRepository departmentRepository, IDepartmentServices departmentServices)
        {
            _departmentRepository = departmentRepository;
            _departmentServices = departmentServices;
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
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllDepartment()
        {
            return Ok(await _departmentRepository.GetAllDepartments());
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
        public async Task<ActionResult<Employee>> GetDepartmentById(int id)
        {
            var employee = await _departmentRepository.GetDepartmentById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddDepartment(Department department)
        {
            // Validate the department model first
            if (department == null)
            {
                return BadRequest("Department data is required.");
            }

            if (string.IsNullOrEmpty(department.Deptname))
            {
                return BadRequest("Department name is required.");
            }

            var createdDepartment = await _departmentRepository.AddDepartment(department);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Deptno }, createdDepartment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            if (id != department.Deptno)
            {
                return BadRequest();
            }

            var updatedDepartment = await _departmentRepository.UpdateDepartment(id, department);
            if (updatedDepartment == null)
            {
                return NotFound();
            }
            return Ok(updatedDepartment);
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
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentRepository.DeleteDepartment(id);
            return Ok($"Succesfully delete department with id : {id}");
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
        [HttpGet("{deptNo}/employees")]
        public async Task<IActionResult> GetEmployee(int deptNo)
        {
            var res = await _departmentRepository.GetEmployee(deptNo);
            return Ok(res);
        }
    }
}
