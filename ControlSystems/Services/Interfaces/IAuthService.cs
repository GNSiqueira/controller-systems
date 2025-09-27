using ControlSystems.Controllers.Dtos;

namespace ControlSystems.Services.Interfaces;

public interface IAuthService
{
    Task<string> Login(LoginRequest login);
    Task LogoutDevicesByUsers();
    Task<string> ReloadToken();

}
