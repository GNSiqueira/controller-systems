using ControlSystems.Objects.Models;

namespace ControlSystems.Data.Interfaces;

public interface IEmpresaRepository
{
    Task<Empresa> GetEmpresaByFuncionario(int id);
}
