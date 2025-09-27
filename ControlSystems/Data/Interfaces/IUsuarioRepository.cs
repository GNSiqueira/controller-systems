using ControlSystems.Data.Repositories;
using ControlSystems.Objects.Models;

namespace ControlSystems.Data.Interfaces;

public interface IUsuarioRepository : IGenericRepositoryRead<Usuario>
{
    Task<Usuario> GetUserByLogin(string login, string pass);
}
