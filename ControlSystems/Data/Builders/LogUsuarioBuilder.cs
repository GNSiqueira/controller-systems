using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Builders;

public class LogUsuarioBuilder : IGenericBuilder
{
    public static void Build(ModelBuilder builder)
    {
        builder.Entity<LogUsuario>(model =>
        {
            model.HasKey(a => a.Id);

            model.Property(a => a.MetricaName).HasMaxLength(100).IsRequired();

            model.Property(a => a.QuantidadeConsumida).IsRequired();

            model.Property(a => a.DataEvento).IsRequired();

            model.HasOne(a => a.Dispositivo)
                .WithMany(a => a.LogsUsuario)
                .HasForeignKey(a => a.DispositivoId)
                .OnDelete(DeleteBehavior.Cascade);

            model.HasData(new List<LogUsuario>
            {
                new LogUsuario(1, "gerecao_relatorio", 2, new DateTime(2025, 9, 21, 10, 30, 0, DateTimeKind.Utc), 1)
            });

        });
    }
}
