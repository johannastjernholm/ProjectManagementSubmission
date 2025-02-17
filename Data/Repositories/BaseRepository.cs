using Data.DataContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;
//Base repositoryn hanterar funktionalitet nära databasen
public abstract class BaseRepository<TEntity>(DataContext context) where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _db = context.Set<TEntity>();

    //Lägga till ny entitet
    public async Task AddAsync(TEntity entity)
    {
        await _db.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    //Hämta läsabar lista över alla
    public async Task<IEnumerable<TEntity>> GetAsync()
    {
        var entities = await _db.ToListAsync();
        return entities;
    }
    //Hämta en särskild entitet
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _db.FirstOrDefaultAsync(expression);
        return entity;
    }
    //Uppdatera entitet
    public async Task UpdateAsync(TEntity entity)
    {
        _db.Update(entity);
        await _context.SaveChangesAsync();

    }
    //Radera entitet
    public async Task RemoveAsync(TEntity entity)
    {
        _db.Remove(entity);
        await _context.SaveChangesAsync();
    }









}
