using ControlSystems.Objects.Enums;

namespace ControlSystems.Objects.Dtos.Entities;

public class UsuarioUpdateDTO
{
    public string? Nome { get; set; }

    public string? Email { get; set; }

    public TipoUsuario? TipoUsuario { get; set; }

    public YesNo? Status { get; set; }

    public int? EmpresaId { get; set; }
}
