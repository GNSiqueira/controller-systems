using ControlSystems.Objects.Models;

namespace ControlSystems.Data.Interfaces;

public interface IDispositivoRepository : IGenericRepositoryWrite<Dispositivo>
{
    Task DeslogarDispositivos(int idUser);
    Task<Dispositivo> GetByName(string name);
}
