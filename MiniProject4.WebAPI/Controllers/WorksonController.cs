using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiniProject4.Application.Interfaces;
using MiniProject4.Application.Services;
using MiniProject4.Domain.Entities;
using MiniProject4.Domain.Interfaces;

namespace MiniProject4.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class WorksonController : ControllerBase
    {
        private readonly IWorksonRepository _worksonRepository;
        private readonly IWorksonService _worksonService;

        public WorksonController(IWorksonRepository worksonRepository, IWorksonService worksonService)
        {
            _worksonRepository = worksonRepository;
            _worksonService = worksonService;
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
        ///     GET /api/v1/Workson
        ///     
        ///     OR
        ///     
        ///     GET /api/v1/Workson/{id}
        ///    
        ///     OR
        ///     
        ///     DELETE /api/v1/Workson/{id}
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workson>>> GetAllWorkOn()
        {
            var res = await _worksonRepository.GetAllWorkOn();
            return Ok(res);
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
        ///     GET /api/v1/Workson
        ///     
        ///     OR
        ///     
        ///     GET /api/v1/Workson/{empno}/{projNo}
        ///    
        ///     OR
        ///     
        ///     DELETE /api/v1/Workson/{id}
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
        [HttpGet("{empNo}/{projNo}")]
        public async Task<ActionResult<Workson>> GetWorkOnById(int empNo, int projNo)
        {
            var project = await _worksonRepository.GetWorkOnById(empNo, projNo);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<Workson>> AddWorkOn(Workson workson)
        {
            var createdWorkson = await _worksonRepository.AddWorkOn(workson);
            return Ok(createdWorkson);
            //return CreatedAtAction(nameof(GetWorkOnById), new { id = createdWorkson.Projno }, createdWorkson);
        }

        [HttpPut("{empNo}/{projNo}")]
        public async Task<IActionResult> UpdateWorkOn(int empNo, int projNo, Workson workson)
        {
            if (empNo != workson.Empno)
            {
                return BadRequest();
            }

            var updatedWorkson = await _worksonRepository.UpdateWorkOn(empNo, projNo, workson);
            if (updatedWorkson == null)
            {
                return NotFound();
            }
            return Ok(updatedWorkson);
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
        ///     GET /api/v1/Workson
        ///     
        ///     OR
        ///     
        ///     GET /api/v1/Workson/{empno}/{projNo}
        ///    
        ///     OR
        ///     
        ///     DELETE /api/v1/Workson/{id}
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
        [HttpDelete("{empNo}/{projNo}")]
        public async Task<IActionResult> DeleteWorkOn(int empNo, int projNo)
        {
            var deleted = await _worksonRepository.DeleteWorkOn(empNo, projNo);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPut("add-workon-entry")]
        public async Task<IActionResult> AddWorkEntry([FromQuery] int empNo, [FromQuery] int projNo, [FromQuery] int hoursWorked, [FromBody] Workson workson)
        {
            try
            {
                await _worksonService.AddWorkEntryAsync(empNo, projNo, hoursWorked, workson);
                return Ok("Work entry added successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
