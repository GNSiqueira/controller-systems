
using ControlSystems.Data.Builders;
using ControlSystems.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlSystems.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Assinatura> Assinaturas { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<ItemPlano> ItensPlano { get; set; }
    public DbSet<LogUsuario> LogsUsuario { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<Plano> Planos { get; set; }
    public DbSet<Sistema> Sistemas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        AssinaturaBuilder.Build(modelBuilder);
        EmpresaBuilder.Build(modelBuilder);
        ItemPlanoBuilder.Build(modelBuilder);
        LogUsuarioBuilder.Build(modelBuilder);
        PagamentoBuilder.Build(modelBuilder);
        PlanoBuilder.Build(modelBuilder);
        SistemaBuilder.Build(modelBuilder);
        UsuarioBuilder.Build(modelBuilder);
    }
}
