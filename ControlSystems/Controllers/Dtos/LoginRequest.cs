using System;

namespace ControlSystems.Controllers.Dtos;

public class LoginRequest
{
    public string? Login { get; set; }
    public string? Password { get; set; }
}
