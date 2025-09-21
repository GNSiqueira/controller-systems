using System;
using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Enums;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Builders;

public class AssinaturaBuilder : IGenericBuilder
{
    public static void Build(ModelBuilder builder)
    {
        builder.Entity<Assinatura>(model =>
        {
            model.HasKey(a => a.Id);

            model.Property(a => a.Status).IsRequired();

            model.Property(a => a.DataInicio).IsRequired();

            model.Property(a => a.DataFim).IsRequired();

            model.Property(a => a.DataCancelamento);

            model.Property(a => a.Valor).IsRequired().HasPrecision(10, 2);

            model.Property(a => a.Created).IsRequired();

            model.HasMany(a => a.Pagamentos)
                .WithOne(p => p.Assinatura)
                .HasForeignKey(p => p.AssinaturaId)
                .OnDelete(DeleteBehavior.Cascade);

            model.HasOne(a => a.Plano)
                .WithMany(p => p.Assinaturas)
                .HasForeignKey(a => a.PlanoId);

            model.HasOne(a => a.Empresa)
                .WithMany(e => e.Assinaturas)
                .HasForeignKey(a => a.EmpresaId);

            model.HasData(new List<Assinatura>
            {
                new Assinatura(1, StatusAssinatura.AGUARDANDO_PAGAMENTO,new DateOnly(2025, 9, 21), new DateOnly(2025, 9, 21), default(DateOnly), 2000.00m, 1, 1, new DateOnly(2025, 09, 21))
            });
        });
    }
}
