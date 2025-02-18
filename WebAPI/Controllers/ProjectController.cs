using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
// Controllers skapade med hjälp av ChatGPT

[Route("api/[controller]")]
[ApiController]
public class ProjectController(ProjectService projectService) : ControllerBase
{
    private readonly ProjectService _projectService = projectService;

    /// <summary>
    /// Skapa nytt projekt
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> CreateProject([FromBody] ProjectRegistrationForm form)
    {
        if (form == null)
        {
            return BadRequest("Formulärdata saknas");

        }

        await _projectService.CreateProjectAsync(form);
        return Ok("Projekt sparat");
    }

    /// <summary>
    /// Hämta aalla projekt
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        return Ok(await _projectService.GetProjectsAsync());
    }
    /// <summary>
    /// Hämta projekt på id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetProjectById(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }
    /// <summary>
    /// Uppdatera ett projekt, id som parameter
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProject(int id, [FromBody] Project project)
    {
        project.Id = id;

        var result = await _projectService.UpdateProjectAsync(project);
        if (!result)
        {
            return NotFound("Projekt finns inte");
        }
        return Ok("Uppdaterat projekt");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProject(int id)
    {
        var result = await _projectService.DeleteProjectAsync(id);
        if (!result)
        {
            return NotFound("Projekt hittas inte");
        }

        return Ok("Projekt raderat");
    }

}
