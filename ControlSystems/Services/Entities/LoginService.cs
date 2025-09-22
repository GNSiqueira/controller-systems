using System;
using ControlSystems.Authentication;
using ControlSystems.Controllers.Dtos;
using ControlSystems.Services.Interfaces;
using ControlSystems.Services.Utils;

namespace ControlSystems.Services.Entities;

public class LoginService : ILoginService
{
    private JwtService Token;

    public LoginService(JwtService token)
    {
        Token = token;
    }

    public Task<List<InfoToken>> GetInfo()
    {
        var info = Token.GetInfoToken();

        return Task.FromResult(info);
    }

    public async Task<string> Login(LoginRequest login)
    {
        var infos = new List<InfoToken>
        {
            new() { Name = "teste", Value = "teste" },
            new() { Name = "teste2", Value = "teste2" },
            new() { Name = "teste3", Value = "teste3" }
        };

        return Token.GenerateJwtToken(infos);

    }
}
