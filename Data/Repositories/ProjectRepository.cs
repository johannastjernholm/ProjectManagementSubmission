using Data.DataContexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context)
{
    //Create
    public ProjectEntity CreateProject(ProjectEntity projectEntity)
    {
        context.Projects.Add(projectEntity);
        context.SaveChanges();

        return projectEntity;
    }

    //Read
    public IEnumerable<ProjectEntity> GetProjects()
    {
        return context.Projects.ToList();
    }

    //Update
    public ProjectEntity UpdateProject(ProjectEntity projectEntity)
    {
        context.Projects.Update(projectEntity);
        context.SaveChanges();
        return projectEntity;
    }

    //Delete
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            context.Projects.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }
}
