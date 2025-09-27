using ControlSystems.Objects.Models;

namespace ControlSystems.Data.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> GetUserByLogin(string login, string pass);
}
