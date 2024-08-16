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
    public class WorksonController : ControllerBase
    {
        private readonly IWorksonRepository _worksonRepository;
        private readonly IWorksonService _worksonService;

        public WorksonController(IWorksonRepository worksonRepository, IWorksonService worksonService)
        {
            _worksonRepository = worksonRepository;
            _worksonService = worksonService;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Workson>>> GetAllWorkOn()
        {
            return Ok(await _worksonRepository.GetAllWorkOn());
        }

        [HttpGet("{empNo}/{projNo}")]
        [MapToApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Workson>> AddWorkOn(Workson workson)
        {
            var createdWorkson = await _worksonRepository.AddWorkOn(workson);
            return Ok(createdWorkson);
            //return CreatedAtAction(nameof(GetWorkOnById), new { id = createdWorkson.Projno }, createdWorkson);
        }

        [HttpPut("{empNo}/{projNo}")]
        [MapToApiVersion("1.0")]
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
            return NoContent();
        }

        [HttpDelete("{empNo}/{projNo}")]
        [MapToApiVersion("1.0")]
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
