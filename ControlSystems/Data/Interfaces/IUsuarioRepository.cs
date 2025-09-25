using ControlSystems.Objects.Models;

namespace ControlSystems.Data.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> GetByLogin(string login, string pass);
}
