using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Repositories;

public class DispositivoRepository : GenericRepositoryWrite<Dispositivo>, IDispositivoRepository
{
    private readonly AppDbContext _context;

    private readonly DbSet<Dispositivo> _db;

    public DispositivoRepository(AppDbContext context) : base(context)
    {
        this._context = context;
        this._db = _context.Set<Dispositivo>();
    }


    public async Task DeslogarDispositivos(int idUser)
    {
        await _db.Where(d => d.UsuarioId == idUser).ExecuteUpdateAsync(s => s.SetProperty(d => d.Logado, Objects.Enums.YesNo.NO));
    }

    public async Task<Dispositivo> GetByName(string name)
    {
        return await _db.Where(d => d.Informacao == name).FirstOrDefaultAsync();
    }
}
