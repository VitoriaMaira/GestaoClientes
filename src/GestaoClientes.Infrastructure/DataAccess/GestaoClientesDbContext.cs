using GestaoClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace GestaoClientes.Infrastructure.DataAccess;
public sealed class GestaoClientesDbContext(DbContextOptions<GestaoClientesDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();
    protected override void OnModelCreating(ModelBuilder builder) => builder.ApplyConfigurationsFromAssembly(typeof(GestaoClientesDbContext).Assembly);
}
