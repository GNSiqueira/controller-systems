using System;
using ControlSystems.Objects.Dtos.DataAnnotations.Validation;

namespace ControlSystems.Objects.Dtos.Entities;

public class LoginRequestDTO
{
    [ValidationRequired]
    public string? Login { get; set; }
    [ValidationRequired]
    public string? Password { get; set; }
    [ValidationRequired]
    public string? Dispositivo { get; set; }
}
