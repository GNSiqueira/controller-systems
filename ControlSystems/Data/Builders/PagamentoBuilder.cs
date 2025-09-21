using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Enums;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Builders;

public class PagamentoBuilder : IGenericBuilder
{
    public static void Build(ModelBuilder builder)
    {
        builder.Entity<Pagamento>(model =>
        {
            model.HasKey(a => a.Id);

            model.Property(a => a.Valor).HasPrecision(10, 2).IsRequired();

            model.Property(a => a.Tipo).IsRequired();

            model.Property(a => a.Status).IsRequired();

            model.Property(a => a.DataPagamento).IsRequired();

            model.Property(a => a.Created).IsRequired();

            model.HasOne(a => a.Assinatura)
                .WithMany(a => a.Pagamentos)
                .HasForeignKey(a => a.AssinaturaId)
                .OnDelete(DeleteBehavior.Cascade);

            model.HasData(new List<Pagamento>
            { 
                new Pagamento(1, 2000.00m, TipoPagamento.PIX, StatusPagamento.SUCESSO, new DateTime(2025, 9, 21, 10, 30, 0, DateTimeKind.Utc), 1, new DateOnly(2025, 9, 19))
            });
        });
    }
}
