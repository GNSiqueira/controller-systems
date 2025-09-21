using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Enums;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Builders;

public class PlanoBuilder : IGenericBuilder
{
    public static void Build(ModelBuilder builder)
    {
        builder.Entity<Plano>(model =>
        {
            model.HasKey(a => a.Id);

            model.Property(a => a.Nome).HasMaxLength(100).IsRequired();

            model.Property(a => a.Descricao).HasColumnType("TEXT");

            model.Property(a => a.Tipo).IsRequired();

            model.Property(a => a.Status).HasDefaultValue(true).IsRequired();

            model.Property(a => a.IsPublic).HasDefaultValue(true).IsRequired();

            model.Property(a => a.IntervaloCobranca).IsRequired();

            model.Property(a => a.Intervalo).IsRequired();

            model.Property(a => a.Created).IsRequired();

            model.HasOne(a => a.Sistema)
                .WithMany(a => a.Planos)
                .HasForeignKey(a => a.SistemaId)
                .OnDelete(DeleteBehavior.Restrict);

            model.HasMany(a => a.ItensPlano)
                .WithOne(a => a.Plano)
                .HasForeignKey(a => a.PlanoId)
                .OnDelete(DeleteBehavior.Cascade);

            model.HasMany(a => a.Assinaturas)
                .WithOne(a => a.Plano)
                .HasForeignKey(a => a.PlanoId)
                .OnDelete(DeleteBehavior.Restrict);

            model.HasData(new List<Plano>
            {
                new(1, "Plano teste", "", TipoPlano.RECORRENTE, true, true,TipoIntervalo.MES, 1, new DateOnly(2025, 09, 21), 1)
            });
        });
    }
}
