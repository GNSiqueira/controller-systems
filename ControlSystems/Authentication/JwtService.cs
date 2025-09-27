
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

        const int minimumKeySizeInBytes = 32; // 256 bits
        if (string.IsNullOrEmpty(keyString) || Encoding.UTF8.GetBytes(keyString).Length < minimumKeySizeInBytes)
        {
            throw new ArgumentException($"A chave JWT (JwtSettings:Key) deve ser configurada e ter no mínimo {minimumKeySizeInBytes} bytes.");
        }
        
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
                double.Parse(jwtSettings["ExpireMinutes"])
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

    private string ExtractToken(string token)
    {
        if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
            throw new ArgumentException("Token não fornecido ou inválido.");

        var returnToken = token.Replace("Bearer ", "");

        return returnToken;
    }
}