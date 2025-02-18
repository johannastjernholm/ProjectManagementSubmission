using Business.Factories;
using Business.Models;
using Data.Repositories;

namespace Business.Services;
// Koden skriver med hjälp av ChatGPT
public class ProjectService(ProjectRepository projectRepository)
{
    private readonly ProjectRepository _projectRepository = projectRepository;

    //Lägg till datan vi får in från formuläret
    public async Task CreateProjectAsync(ProjectRegistrationForm form)
    {
        var projectEntity = ProjectFactory.Create(form);
        if (projectEntity == null)
        {
            return;
        }

        await _projectRepository.AddAsync(projectEntity);

    }
    //Hämta alla projects
    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        //Hämta entiteter från databasen via repository
        var projectEntities = await _projectRepository.GetAsync();

        return projectEntities.Select(ProjectFactory.Create)!;
    }

    //Hämta project med id
    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);

        if (projectEntity == null)
        {
            return null;
        }

        return ProjectFactory.Create(projectEntity);
    }
    //Uppdatera projekt
    public async Task<bool> UpdateProjectAsync(Project project)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Id == project.Id);
        
        if (projectEntity == null)
        {
            return false;
        }
       
        projectEntity.Description = project.Description;
        projectEntity.Notes = project.Notes;
        projectEntity.StartDate = project.StartDate;
        projectEntity.EndDate= project.EndDate;
        projectEntity.Status = project.Status;
        projectEntity.CustomerId = project.CustomerId;
               
        await _projectRepository.UpdateAsync(projectEntity);

        return true;

    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);
        if (projectEntity == null)
        {
            return false;
        }
        await _projectRepository.RemoveAsync(projectEntity);
        return true;
    }
}
