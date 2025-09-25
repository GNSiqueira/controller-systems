using System;
using ControlSystems.Authentication;
using ControlSystems.Controllers.Dtos;
using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Models;
using ControlSystems.Services.Interfaces;
using ControlSystems.Services.Utils;

namespace ControlSystems.Services.Entities;

public class LoginService : ILoginService
{
    private JwtService _token;

    private IUsuarioRepository _repository;

    public LoginService(JwtService token, IUsuarioRepository repository)
    {
        _token = token;
        _repository = repository;
    }

    public Task<List<InfoToken>> GetInfo()
    {
        var info = _token.GetInfoToken();

        return Task.FromResult(info);
    }

    public async Task<string> Login(LoginRequest login)
    {
        var user = _repository.GetByLogin(login.Login, login.Password);
        return "dafsdf";
    }

    private Usuario ValidateUser()
    {
        return new Usuario();
    }
}
