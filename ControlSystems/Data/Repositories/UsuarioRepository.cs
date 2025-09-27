using System;
using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    private readonly DbSet<Usuario> _db;

    public UsuarioRepository(AppDbContext context)
    {
        this._context = context;

        this._db = _context.Set<Usuario>();
    }

    public async Task<Usuario> GetUserByLogin(string login, string pass)
    {
        return await _db
            .Include(u => u.Empresa)
                .ThenInclude(u => u.Assinaturas)
                    .ThenInclude(u => u.Pagamentos)
            .Include(u => u.Empresa)
                .ThenInclude(u => u.Assinaturas)
                    .ThenInclude(u => u.Plano)
                        .ThenInclude(u => u.Sistema)
            .FirstOrDefaultAsync(p => p.Email == login && p.Password == pass);
    }
}
