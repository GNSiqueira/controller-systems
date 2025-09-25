using System;
using ControlSystems.Objects.Dtos.DataAnnotations.Valid;

namespace ControlSystems.Controllers.Dtos;

public class LoginRequest
{
    [NullOrEmpty]
    public string? Login { get; set; }
    [NullOrEmpty]
    public string? Password { get; set; }
    [NullOrEmpty]
    public string? Dispositivo { get; set; }
}
