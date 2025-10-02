using System.Text.Json;
using AutoMapper;
using ControlSystems.Authentication;
using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Contracts.Exceptions.Exceptions;
using ControlSystems.Objects.Dtos.Entities;
using ControlSystems.Objects.Models;
using ControlSystems.Services.Interfaces;
using ControlSystems.Utils;

namespace ControlSystems.Services.Entities;

public class UsuarioService : IUsuarioService
{
    private IUsuarioRepository _repository;
    private IMapper _mapper;
    private JwtService _token;

    public UsuarioService(IUsuarioRepository repository, IMapper mapper, JwtService token)
    {
        this._repository = repository;
        this._mapper = mapper;
        this._token = token;
    }

    public async Task<UsuarioDTO> GetUserByToken()
    {
        var idusuario = _token.GetInfoToken().Find(a => a.Name == "id").Value;

        if (idusuario == null)
            throw new ExceptionBadRequest("Usuário não fornecido corretamente!");

        var usuario = await _repository.GetById(Convert.ToInt32(idusuario));

        if (usuario == null)
            throw new ExceptionNotFound("Usuário não encontrado!");

        return _mapper.Map<UsuarioDTO>(usuario);

    }

    public async Task UpdateSenha(UsuarioSenhaDTO usuario)
    {
        var idusuario = Convert.ToInt32(_token.GetInfoToken().Find(a => a.Name == "id").Value);

        if (idusuario == 0 || idusuario == null)
            throw new ExceptionBadRequest("Usuário não fornecido corretamente!");

        var user = await _repository.GetById(idusuario);

        _mapper.Map(usuario, user);

        await _repository.Update(user);
    }

    public async Task UpdateUsuario(UsuarioUpdateDTO usuario)
    {
        var idusuario = Convert.ToInt32(_token.GetInfoToken().Find(a => a.Name == "id").Value);

        if (idusuario == 0 || idusuario == null)
            throw new ExceptionBadRequest("Usuário não fornecido corretamente!");

        var user = await _repository.GetById(idusuario);

        _mapper.Map(usuario, user);

        await _repository.Update(user);
    }
}
