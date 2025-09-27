using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Enums;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Builders;

public class ItemPlanoBuilder : IGenericBuilder
{
    public static void Build(ModelBuilder builder)
    {
        builder.Entity<ItemPlano>(model =>
        {
            model.HasKey(a => a.Id);

            model.Property(a => a.Metrica).HasMaxLength(100);

            model.Property(a => a.Tipo).IsRequired();

            model.Property(a => a.Limite);

            model.Property(a => a.Descricao).IsRequired().HasColumnType("TEXT");

            model.Property(a => a.Fixo).IsRequired();

            model.Property(a => a.Valor).HasPrecision(10, 2).IsRequired();

            model.Property(a => a.Moeda).HasMaxLength(3).IsRequired();

            model.Property(a => a.Created).IsRequired();

            model.HasOne(a => a.Plano)
                .WithMany(a => a.ItensPlano)
                .HasForeignKey(a => a.PlanoId)
                .OnDelete(DeleteBehavior.Cascade);

            model.HasData(new List<ItemPlano>
            {
                new (1, string.Empty, TipoItemPlano.RECORRENTE, 0, "É um item de teste.", YesNo.YES, 243.56m, "BRL", 1, new DateOnly(2025, 09, 21))
            });
        });
    }
}
