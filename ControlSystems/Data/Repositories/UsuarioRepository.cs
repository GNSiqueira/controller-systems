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

    public async Task<Usuario> GetByLogin(string login, string pass)
    {
        return await _db.FirstOrDefaultAsync(p => p.Email == login && p.Password == pass);
    }
}
