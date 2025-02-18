using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;
// Koden skriver med hjälp av ChatGPT
public class ProjectService(ProjectRepository projectRepository, CustomerRepository customerRepository)
{
    private readonly ProjectRepository _projectRepository = projectRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;

    //Lägg till datan vi får in från formuläret
    public async Task CreateProjectAsync(ProjectRegistrationForm form)
    {
        var customer = await _customerRepository.GetAsync(x => x.CustomerName == form.CustomerName);
        //Kontrollera så kund finns i databasen innan nytt projekt skapas
        if (customer == null)
        {
            customer = new CustomerEntity { CustomerName = form.CustomerName };
            await _customerRepository.AddAsync(customer);

        }

        var projectEntity = ProjectFactory.Create(form);
        if (projectEntity == null)
        {
            throw new Exception("Misslyckades att skapa projekt");
        }


        projectEntity.CustomerId = customer.Id;

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
        projectEntity.EndDate = project.EndDate;
        projectEntity.Status = project.Status;
        projectEntity.CustomerId = project.CustomerId;

        await _projectRepository.UpdateAsync(projectEntity);

        return true;

    }
    //Radera ett projekt
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
