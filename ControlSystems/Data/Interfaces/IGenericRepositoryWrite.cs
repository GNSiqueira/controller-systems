using System;

namespace ControlSystems.Data.Interfaces;

public interface IGenericRepositoryWrite<T> where T : class
{
    Task Create(T entity);
    Task Update(T entity);
}
