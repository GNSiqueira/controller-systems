using ControlSystems.Objects.Dtos.DataAnnotations.Validation;
namespace ControlSystems.Objects.Dtos.Entities;

public class UsuarioSenhaDTO
{
    [ValidationRequired]
    public string? Password { get; set; }
}
