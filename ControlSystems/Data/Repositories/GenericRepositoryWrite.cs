using System;
using ControlSystems.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Repositories;

public class GenericRepositoryWrite<T> : IGenericRepositoryWrite<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepositoryWrite(AppDbContext context)
    {
        this._context = context;
        this._dbSet = _context.Set<T>();
    }

    public async Task Create(T entity)
    {
        await _dbSet.AddAsync(entity);

        await SaveChanges();
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task Update(T entity)
    {
        // Recupera a chave primária (supondo que seja 'Id')
        var entityId = _context.Entry(entity).Property("Id").CurrentValue;

        // Verifica se a entidade com o mesmo Id já está sendo rastreada
        var trackedEntity = _context.ChangeTracker.Entries<T>()
            .FirstOrDefault(e => e.Property("Id").CurrentValue.Equals(entityId));

        // Se a entidade já estiver sendo rastreada, desanexa
        if (trackedEntity != null)
        {
            _context.Entry(trackedEntity.Entity).State = EntityState.Detached;
        }

        // Anexa a nova entidade e marca como 'Modified'
        _context.Entry(entity).State = EntityState.Modified;

        // Salva as alterações no banco de dados
        await SaveChanges();
    }
}
