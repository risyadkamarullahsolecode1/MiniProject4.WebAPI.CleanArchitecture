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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workson>>> GetAllWorkOn()
        {
            return Ok(await _worksonRepository.GetAllWorkOn());
        }

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
            return NoContent();
        }

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
        [HttpPost("Max-Hours/{empNo}/{projNo}/{hoursWorked}")]
        public async Task<ActionResult<bool>> MaxHoursEmployeeToProject(int empNo, int projNo, int hoursWorked)
        {
            try
            {
                var result = await _worksonService.MaxHoursEmployeeToProject(empNo, projNo, hoursWorked);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("Max-Employee")]
        public async Task<IActionResult> MaxEmployeeToProject(int empNo, int projNo, Workson workson)
        {
            try
            {
                await _worksonService.MaxEmployeeToProject(empNo, projNo, workson);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
