using System;

namespace ControlSystems.Data.Interfaces;

public interface IGenericRepository<T>
{
    Task<T> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}
