using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Enums;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Builders;

public class UsuarioBuilder : IGenericBuilder
{
    public static void Build(ModelBuilder builder)
    {
        builder.Entity<Usuario>(model =>
        {
            model.HasKey(a => a.Id);

            model.Property(a => a.Nome).HasMaxLength(100).IsRequired();

            model.Property(a => a.Email).HasMaxLength(250).IsRequired();

            model.Property(a => a.Password).HasMaxLength(255).IsRequired();

            model.Property(a => a.TipoUsuario).IsRequired();

            model.Property(a => a.Status).HasDefaultValue(true).IsRequired();

            model.Property(a => a.Created).IsRequired();

            model.HasMany(a => a.Dispositivos)
                .WithOne(a => a.Usuario)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            model.HasOne(a => a.Empresa)
                .WithMany(a => a.Usuarios)
                .HasForeignKey(a => a.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade);

            model.HasData(new List<Usuario>
            {
                new(1, "Neto", "gabriel@gmail.com", "hash_de_senha_segura_aqui", TipoUsuario.ADMIN, true, new DateOnly(2025, 9, 21), 1)

            });
        });
    }
}
