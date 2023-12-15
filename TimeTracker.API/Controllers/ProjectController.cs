using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace TimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _ProjectService;

        public ProjectController(IProjectService projectService)
        {
            _ProjectService = projectService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProjectResponse>>> GetAllProjects()
        {
            return Ok(await _ProjectService.GetAllProjects());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectResponse>> GetTimeEntryById(int id)
        {
            var result = await _ProjectService.GetProjectById(id);
            if (result == null)
            {
                return NotFound("Project with the given ID was not found!");
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<List<ProjectResponse>>> CreateProject(ProjectCreateRequest project)
        {
            var result = await _ProjectService.CreateProject(project);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<ProjectResponse>>> UpdateProject(int id, ProjectUpdateRequest project)
        {
            var result = await _ProjectService.UpdateProject(id, project);
            if (result == null)
            {
                return NotFound("Project with the given ID was not found!");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProjectResponse>>> DeleteProject(int id)
        {
            var result = await _ProjectService.DeleteProject(id);
            if (result == null)
            {
                return NotFound("Project with the given ID was not found!");
            }
            return Ok(result);
        }
    }
}
