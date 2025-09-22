using ControlSystems.Controllers.Dtos;
using ControlSystems.Services.Utils;

namespace ControlSystems.Services.Interfaces;

public interface ILoginService
{
    Task<string> Login(LoginRequest login);
    Task<List<InfoToken>> GetInfo();
}
