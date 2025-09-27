
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ControlSystems.Services.Utils;
using ControlSystems.Objects.Contracts.Exceptions.Exceptions;

namespace ControlSystems.Authentication;

public class JwtService : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GenerateJwtToken(List<InfoToken> infoToken)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var keyString = jwtSettings["Key"];
        if (string.IsNullOrEmpty(keyString))
            throw new ArgumentException("JwtSettings:Key não está configurado.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();

        foreach (InfoToken item in infoToken)
        {
            // CORREÇÃO 1: Adiciona a Claim com Tipo e Valor
            claims.Add(new Claim(item.Name, item.Value));
        }

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.TryParse(jwtSettings["ExpireMinutes"], out var expireMinutes) ? expireMinutes : 60
            ),
            // expires: DateTime.MaxValue,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public List<InfoToken> GetInfoToken()
    {
        var tokenHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(tokenHeader))
            throw new ExceptionBadRequest("Cabeçalho de autorização não fornecido.");
        string token = tokenHeader!;

        token = ExtractToken(token);

        var header = new JwtSecurityTokenHandler();

        var jwtToken = header.ReadJwtToken(token);

        var claims = jwtToken.Claims;

        var infos = new List<InfoToken>();

        foreach (var item in claims)
            infos.Add(new InfoToken { Name = item.Type, Value = item.Value });

        return infos;
    }

    public string ExtractToken(string token)
    {
        if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
            throw new ArgumentException("Token não fornecido ou inválido.");

        var returnToken = token.Replace("Bearer ", "");

        return returnToken;
    }
}