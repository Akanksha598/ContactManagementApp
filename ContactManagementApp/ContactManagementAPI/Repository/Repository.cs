using ContactManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementAPI.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;

    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }
    public async Task CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await SaveAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        IQueryable<T> query = dbSet;

        return await query.ToListAsync();
    }

    public async Task<T> GetAsync(bool tracked = true)
    {
        IQueryable<T> query = dbSet;

        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        dbSet.Remove(entity);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}
