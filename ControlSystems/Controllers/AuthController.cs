using ControlSystems.Controllers.Dtos;
using ControlSystems.Objects.Contracts;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;
using ControlSystems.Services.Interfaces;
using ControlSystems.Services.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystems.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController : Controller
{

    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPatch]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        Execute.Executar(login);
        return Response<string>.Ok(await _service.Login(login), "Token de validação");
    }

    [HttpPost]
    public async Task<IActionResult> LogoutAllDevices()
    {
        await _service.LogoutDevicesByUsers();
        return Response<object>.NoContent();
    }
}