
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ControlSystems.Services.Utils;

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
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
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
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"])),
            // expires: DateTime.MaxValue,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public List<InfoToken> GetInfoToken()
    {
        string token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];

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