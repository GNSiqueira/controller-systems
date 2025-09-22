using System;
using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Builders;

public class DispositivoBuilder : IGenericBuilder
{
    public static void Build(ModelBuilder builder)
    {
        builder.Entity<Dispositivo>(model =>
        {
            model.HasKey(a => a.Id);

            model.Property(a => a.Informacao).IsRequired().HasMaxLength(100);

            model.Property(a => a.Logado).IsRequired();

            model.HasMany(a => a.LogsUsuario)
                .WithOne(a => a.Dispositivo)
                .HasForeignKey(a => a.DispositivoId)
                .OnDelete(DeleteBehavior.Restrict);

            model.HasOne(a => a.Usuario)
                .WithMany(a => a.Dispositivos)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            model.HasData(new Dispositivo(1, "acernitro5", true, 1));
        });
    }
}
