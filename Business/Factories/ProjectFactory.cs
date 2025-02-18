using Business.Models;
using Data.Entities;

namespace Business.Factories;
//Koden skriven med hjälp av ChatGPT

public static class ProjectFactory
{
    //Skapa entitet av datan vi får in av användaren
    public static ProjectEntity? Create(ProjectRegistrationForm form) => form == null ? null : new()
    {
        Description = form.Description,
        Notes = form.Notes,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        Status = form.Status,
        CustomerId = 0
    };


    public static Project? Create(ProjectEntity entity) => entity == null ? null : new()
    { 
        Id = entity.Id,
        Description = entity.Description,
        Notes = entity.Notes,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        Status = entity.Status,
        CustomerId = entity.CustomerId,     
    };
}
