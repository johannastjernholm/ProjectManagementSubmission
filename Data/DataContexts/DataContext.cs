
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.DataContexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    //Reg alla entiteter
    public DbSet<CustomerEntity> Customers { get; set; }   
    public DbSet<ProjectEntity> Projects { get; set; }

}
