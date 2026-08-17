using GestaoClientes.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GestaoClientes.Infrastructure.Configurations;
public sealed class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> e)
    {
        e.ToTable("Clientes"); e.HasKey(x => x.Id);
        e.Property(x => x.Nome).HasMaxLength(150).IsRequired();
        e.Property(x => x.Cpf).HasMaxLength(11).IsRequired(); e.HasIndex(x => x.Cpf).IsUnique();
        e.Property(x => x.Email).HasMaxLength(200).IsRequired(); e.HasIndex(x => x.Email).IsUnique();
        e.HasIndex(x => x.Status); e.HasIndex(x => x.Nome); e.Property(x => x.Status).HasConversion<string>();
        e.HasMany(x => x.Enderecos).WithOne().HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Cascade);
    }
}
