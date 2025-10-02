using ControlSystems.Objects.Dtos.Entities;
using ControlSystems.Objects.Models;

namespace ControlSystems.Services.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioDTO> GetUserByToken();
    Task UpdateUsuario(UsuarioUpdateDTO usuario);
    Task UpdateSenha(UsuarioSenhaDTO usuario);
}
