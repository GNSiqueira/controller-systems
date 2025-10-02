using ControlSystems.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlSystems.Data.Repositories;

// Esta classe agora implementa AMBAS as interfaces, unificando a lógica.
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task Create(T entity)
    {
        await _dbSet.AddAsync(entity);

        await SaveChanges();
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);

        await SaveChanges();
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await SaveChanges();
    }
}