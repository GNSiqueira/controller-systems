using ControlSystems.Controllers.Dtos;
using ControlSystems.Objects.Contracts;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;
using ControlSystems.Services.Interfaces;
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
    [HttpPost("login"), MapToApiVersion("1")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        Execute.Executar(login);
        return Response<string>.Ok(await _service.Login(login), "Token de validação");
    }

    [HttpPatch("logout-all-devices"), MapToApiVersion("1")]
    public async Task<IActionResult> LogoutAllDevices()
    {
        await _service.LogoutDevicesByUsers();
        return Response<object>.NoContent();
    }

    [HttpGet("refresh-token"), MapToApiVersion("1")]
    public async Task<IActionResult> RefreshToken()
    {
        return Response<string>.Ok(await _service.ReloadToken(), "Novo token");
    }
}