// Em: ControlSystems/Data/Builders/EmpresaBuilder.cs

using ControlSystems.Data.Interfaces;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data.Builders;

public class EmpresaBuilder : IGenericBuilder
{
    public static void Build(ModelBuilder builder)
    {
        builder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.RazaoSocial)
                  .HasMaxLength(150)
                  .IsRequired();

            entity.Property(e => e.NomeFantasia)
                  .HasMaxLength(150)
                  .IsRequired();

            entity.Property(e => e.CnpjCpf)
                  .HasMaxLength(14)
                  .IsRequired();

            entity.Property(e => e.Telefone)
                  .HasMaxLength(20);

            entity.Property(e => e.Email)
                  .HasMaxLength(100)
                  .IsRequired();
            entity.HasMany(e => e.Usuarios)
                  .WithOne(u => u.Empresa)
                  .HasForeignKey(u => u.EmpresaId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.Assinaturas)
                  .WithOne(a => a.Empresa)
                  .HasForeignKey(a => a.EmpresaId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(new Empresa
            {
                Id = 1,
                Status = true,
                RazaoSocial = "EMPRESA MODELO LTDA",
                NomeFantasia = "Empresa Modelo",
                CnpjCpf = "12345678000199",
                Telefone = "+5511999998888",
                Email = "contato@empresamodelo.com",
                Created = new DateOnly(2025, 09, 21)
            });
        });
    }
}