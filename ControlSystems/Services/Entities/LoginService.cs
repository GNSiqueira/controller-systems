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

    private IUsuarioRepository _user;

    private IDispositivoRepository _device;

    public LoginService(JwtService token, IUsuarioRepository repository, IDispositivoRepository device)
    {
        _token = token;
        _user = repository;
        _device = device;
    }

    public Task<List<InfoToken>> GetInfo()
    {
        var info = _token.GetInfoToken();

        return Task.FromResult(info);
    }

    public async Task<string> Login(LoginRequest login)
    {
        var user = await _user.GetUserByLogin(login.Login, login.Password);

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

        datenow = new DateTime(datenow.Year, datenow.Month, datenow.Day, 23, 59, 0);

        if (!new[] { StatusAssinatura.ATIVO, StatusAssinatura.TRIAL, StatusAssinatura.AGUARDANDO_PAGAMENTO }.Contains(ass.Status))
            throw new ExceptionForbidden("Essa assinatura está cancelada ou está inadimplente!");

        if (ass.DataFim < DateOnly.FromDateTime(datenow))
            throw new ExceptionForbidden("Pagamento pendente!");

        await _device.DeslogarDispositivos(user.Id);

        var dispositivo = await _device.GetByName(login.Dispositivo);

        if (dispositivo == null)
            await _device.Create(new Dispositivo(0, login.Dispositivo, YesNo.YES, user.Id));
        else
        {
            dispositivo.Logado = YesNo.YES;
            await _device.Update(dispositivo);
        }

        List<InfoToken> infos = new List<InfoToken> {
            new() { Name = "id", Value = user.Id.ToString()},
            new() { Name = "device", Value = login.Dispositivo}
        };

        return _token.GenerateJwtToken(infos);
    }

}
