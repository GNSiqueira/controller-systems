
using ControlSystems.Objects.Dtos.Entities;

namespace ControlSystems.Services.Interfaces;

public interface IAuthService
{
    Task<string> Login(LoginRequestDTO login);
    Task LogoutDevicesByUsers();
    Task<string> ReloadToken();

}
