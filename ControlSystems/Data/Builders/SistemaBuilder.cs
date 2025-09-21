using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Builders;

public class SistemaBuilder : IGenericBuilder
{
    public static void Build(ModelBuilder builder)
    {
        builder.Entity<Sistema>(model =>
        {
            model.HasKey(a => a.Id);

            model.Property(a => a.Nome).IsRequired().HasMaxLength(100);

            model.Property(a => a.Descricao).HasColumnType("TEXT").IsRequired();

            model.Property(a => a.Status).HasDefaultValue(true).IsRequired();

            model.Property(a => a.Created).IsRequired();

            model.HasMany(a => a.Planos)
                .WithOne(a => a.Sistema)
                .HasForeignKey(a => a.SistemaId)
                .OnDelete(DeleteBehavior.Cascade);

            model.HasData(new List<Sistema>
            {
                new Sistema(1, "Sistema Teste", "Sistema testezinho", true, new DateOnly(2025, 09, 21))
            });
        });
    }
}
