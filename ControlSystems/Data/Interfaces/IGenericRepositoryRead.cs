namespace ControlSystems.Data.Interfaces;

public interface IGenericRepositoryRead<T> where T : class
{
    Task<T> GetById(int id);
}