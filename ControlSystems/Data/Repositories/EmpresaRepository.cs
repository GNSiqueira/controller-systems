using System;
using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly AppDbContext _context;

    private readonly DbSet<Empresa> _db;

    public EmpresaRepository(AppDbContext context)
    {
        this._context = context;

        this._db = _context.Set<Empresa>();
    }

}
