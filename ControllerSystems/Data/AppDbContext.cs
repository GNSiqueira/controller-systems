using System;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // public DbSet<Product> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // ProductBuilder.Build(modelBuilder);
    }
}
