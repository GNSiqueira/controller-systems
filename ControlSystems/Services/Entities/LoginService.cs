using System;
using ControlSystems.Authentication;
using ControlSystems.Controllers.Dtos;
using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Contracts.Exceptions.Exceptions;
using ControlSystems.Objects.Enums;
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
        var user = await _repository.GetUserByLogin(login.Login, login.Password);

        if (user == null || user.Status != YesNo.YES)
            throw new ExceptionBadRequest("Usuário ou senha inválidos.");

        var emp = user.Empresa;

        if (emp == null)
            throw new ExceptionBadRequest("Sua empresa não foi encontrada!");

        if (emp.Status != YesNo.YES)
            throw new ExceptionForbidden("Sua empresa está inativa!");

        var ass = emp.Assinaturas?.FirstOrDefault();

        if (ass == null)
            throw new ExceptionBadRequest("Assinatura não encontrada!");

        var plan = ass.Plano;

        if (plan == null)
            throw new ExceptionBadRequest("Plano não encontrado!");

        if (plan.Status != YesNo.YES)
            throw new ExceptionForbidden("Plano inativo!");

        var sys = plan.Sistema;

        if (sys == null)
            throw new ExceptionBadRequest("Sistema não encontrado!");

        if (sys.Status != YesNo.YES)
            throw new ExceptionForbidden("Sistema inativo!");

        if (ass.Status == StatusAssinatura.CANCELADO)
            throw new ExceptionForbidden("Assinatura com problemas. Inadimplente ou cancelado!");

        var datenow = DateTime.Now;

        // if (new [] {StatusAssinatura.ATIVO, StatusAssinatura.ATIVO, StatusAssinatura.AGUARDANDO_PAGAMENTO}.Contains(ass.Status))
        //     if (ass.DataFim?.ToDateTime(TimeOnly.MinValue) > )
                


        throw new NotImplementedException();
    }

}
