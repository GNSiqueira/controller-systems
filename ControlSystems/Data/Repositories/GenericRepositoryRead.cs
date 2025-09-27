using ControlSystems.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Repositories;

public class GenericRepositoryRead<T> : IGenericRepositoryRead<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepositoryRead(AppDbContext context)
    {
        this._context = context;

        this._dbSet = _context.Set<T>();
    }

    public async Task<T> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }
}