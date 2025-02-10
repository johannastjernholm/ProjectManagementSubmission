

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Data.DataContexts;

public class DataContextFacory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        // Connectionstring
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\ProjectManagementSubmission\Data\DataBases\local_pm_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

        return new DataContext(optionsBuilder.Options);
    }
}
