using ControlSystems.Controllers.Dtos;
using ControlSystems.Objects.Models;
using ControlSystems.Services.Utils;

namespace ControlSystems.Services.Interfaces;

public interface IAuthService
{
    Task<string> Login(LoginRequest login);
    Task LogoutDevicesByUsers();

}
