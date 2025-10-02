using ControlSystems.Objects.Contracts;
using ControlSystems.Objects.Dtos.DataAnnotations.Base;
using ControlSystems.Objects.Dtos.Entities;
using ControlSystems.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControlSystems.Controllers;

[Route("api/v{version:apiVersion}/usuario")]
[ApiController]
[Authorize]
[ApiVersion("1")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuarioController(IUsuarioService service)
    {
        this._service = service;
    }

    [HttpGet, MapToApiVersion("1")]
    public async Task<IActionResult> GetUsuario()
    {
        var user = await _service.GetUserByToken();
        return Response<UsuarioDTO>.Ok(user, "Usuário encontrado com êxito!");
    }

    [HttpPut("senha"), MapToApiVersion("1")]
    public async Task<IActionResult> UpdateSenha([FromBody] UsuarioSenhaDTO senhaDTO)
    {
        Execute.Executar(senhaDTO);
        await _service.UpdateSenha(senhaDTO);
        return Response<object>.Ok(default, "Usuário alterado com sucesso!");
    }

    [HttpPut(), MapToApiVersion("1")]
    public async Task<IActionResult> UpdateUsuario([FromBody] UsuarioUpdateDTO usuario)
    {
        Execute.Executar(usuario);
        await _service.UpdateUsuario(usuario);
        return Response<object>.Ok(default, "Usuário alterado com sucesso!");
    }
}
