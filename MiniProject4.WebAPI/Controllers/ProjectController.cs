using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiniProject4.Application.Interfaces;
using MiniProject4.Domain.Entities;
using MiniProject4.Domain.Interfaces;

namespace MiniProject4.WebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectService _projectService;
        public ProjectController(IProjectRepository projectRepository, IProjectService projectService)
        {
            _projectRepository = projectRepository;
            _projectService = projectService;
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
        ///     GET /api/v1/Project
        ///     
        ///     OR
        ///     
        ///     GET /api/v1/Project/{id}
        ///    
        ///     OR
        ///     
        ///     DELETE /api/v1/Project/{id}
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            return Ok(await _projectRepository.GetAllProjects());
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
        ///     GET /api/v1/Project
        ///     
        ///     OR
        ///     
        ///     GET /api/v1/Project/{id}
        ///    
        ///     OR
        ///     
        ///     DELETE /api/v1/Project/{id}
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _projectRepository.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(Project project)
        {
            var createdProject = await _projectService.AddProjectAsync(project);
            return Ok(createdProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.Projno)
            {
                return BadRequest();
            }

            var updatedProject = await _projectRepository.UpdateProject(id, project);
            if (updatedProject == null)
            {
                return NotFound();
            }
            return Ok(updatedProject);
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
        ///     GET /api/v1/Project
        ///     
        ///     OR
        ///     
        ///     GET /api/v1/Project/{id}
        ///    
        ///     OR
        ///     
        ///     DELETE /api/v1/Project/{id}
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> This endpoint returns a list of Accounts.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _projectRepository.DeleteProject(id);
            return Ok($"Succesfully delete project with id : {id}"); ;
        }

        [HttpGet("managed-byPlanning")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsManagedByPlanning()
        {
            return Ok(await _projectService.GetProjectsManagedByPlanning());
        }
        [HttpGet("project-with-NoEmployee")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsWithNoEmployees()
        {
            return Ok(await _projectService.GetProjectsWithNoEmployees());
        }
        [HttpGet("managed-by-FemaleManagers")]
        public async Task<ActionResult<IEnumerable<object>>> GetProjectsManagedByFemaleManagers()
        {
            return Ok(await _projectService.GetProjectsManagedByFemaleManagers());
        }

        [HttpGet("{projNo}/department")]
        public async Task<IActionResult> GetDepartmentAsync(int projNo)
        {
            var res = await _projectRepository.GetDepartmentAsync(projNo);
            return Ok(res);
        }
    }
}
