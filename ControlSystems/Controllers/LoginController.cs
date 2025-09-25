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
public class LoginController : Controller
{

    private readonly ILoginService _service;

    public LoginController(ILoginService service)
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

    [HttpGet]
    public async Task<IActionResult> GetInfo()
    {
        return Response<List<InfoToken>>.Ok(await _service.GetInfo(), "Informaçẽos que estão dentro do Token.");
    }
}