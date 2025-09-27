using ControlSystems.Controllers.Dtos;
using ControlSystems.Objects.Contracts;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;
using ControlSystems.Services.Interfaces;
using ControlSystems.Services.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystems.Controllers;

[Route("api/v{version:apiVersion}/auth")]
[ApiController]
[Authorize]
[ApiVersion("1")]
public class AuthController : Controller
{

    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPatch("login"), MapToApiVersion("1")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        Execute.Executar(login);
        return Response<string>.Ok(await _service.Login(login), "Token de validação");
    }

    [HttpPost("logout-all-devices"), MapToApiVersion("1")]
    public async Task<IActionResult> LogoutAllDevices()
    {
        await _service.LogoutDevicesByUsers();
        return Response<object>.NoContent();
    }
}