using GestaoClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GestaoClientes.Infrastructure.DataAccess.Mappings;
public sealed class ClienteMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> e)
    {
        e.ToTable("Clientes"); e.HasKey(x => x.Id);
        e.Property(x => x.Nome).HasMaxLength(150).IsRequired(); e.HasIndex(x => x.Nome);
        e.Property(x => x.Cpf).HasMaxLength(11).IsRequired(); e.HasIndex(x => x.Cpf).IsUnique();
        e.Property(x => x.Email).HasMaxLength(200).IsRequired(); e.HasIndex(x => x.Email).IsUnique();
        e.Property(x => x.Telefone).HasMaxLength(20).IsRequired();
        e.Property(x => x.Status).HasConversion<string>(); e.HasIndex(x => x.Status);
        e.HasMany(x => x.Enderecos).WithOne().HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Cascade);
    }
}
