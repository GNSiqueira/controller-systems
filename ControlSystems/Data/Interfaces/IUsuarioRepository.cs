using ControlSystems.Objects.Models;

namespace ControlSystems.Data.Interfaces;

public interface IUsuarioRepository : IGenericRepository<Usuario>
{
    Task<Usuario> GetUserByLogin(string login, string pass);
}
