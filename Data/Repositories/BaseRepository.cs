using Data.DataContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
//Kod skriven vid Hans Mattin-Lassei föreläsning
namespace Data.Repositories;
//Base repositoryn hanterar funktionalitet nära databasen
public abstract class BaseRepository<TEntity>(DataContext context) where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _db = context.Set<TEntity>();

    //Lägga till ny entitet
    public async Task AddAsync(TEntity entity)
    {
        try
        {
            await _db.AddAsync(entity);
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error AddAsync: {ex.Message}");
        }

    }
    //Hämta läsabar lista över alla
    public async Task<IEnumerable<TEntity>> GetAsync()
    {
        try
        {
            var entities = await _db.ToListAsync();
            return entities;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error GetAsync: {ex.Message}");
            return Enumerable.Empty<TEntity>();
        }
    }
    //Hämta en särskild entitet
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await _db.FirstOrDefaultAsync(expression);
            return entity;


        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error GetAsync: {ex.Message}");
            return null;
        }
    }

    //Hämta fler entieter (include())
    public IQueryable<TEntity> GetQueryable()
    {
        try
        {
            return _db.AsQueryable();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error GetQueryable: {ex.Message}");
            return Enumerable.Empty<TEntity>().AsQueryable();
        }
    }
    //Uppdatera entitet
    public async Task UpdateAsync(TEntity entity)
    {
        try
        {
            _db.Update(entity);
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error UpdateAsync: {ex.Message}");
        }
    }
    //Radera entitet
    public async Task RemoveAsync(TEntity entity)
    {
        try
        {

            _db.Remove(entity);
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error RemoveAsync: {ex.Message}");
        }
    }

}
