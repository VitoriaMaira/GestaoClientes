using GestaoClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoClientes.Infrastructure.DataAccess.Mappings;

public sealed class ClienteMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
        builder.HasKey(cliente => cliente.Id);

        builder.Property(cliente => cliente.Nome).HasMaxLength(150).IsRequired();
        builder.HasIndex(cliente => cliente.Nome);

        builder.Property(cliente => cliente.Cpf).HasMaxLength(11).IsRequired();
        builder.HasIndex(cliente => cliente.Cpf).IsUnique();

        builder.Property(cliente => cliente.Email).HasMaxLength(200).IsRequired();
        builder.HasIndex(cliente => cliente.Email).IsUnique();

        builder.Property(cliente => cliente.Telefone).HasMaxLength(20).IsRequired();
        builder.Property(cliente => cliente.Status).HasConversion<string>();
        builder.HasIndex(cliente => cliente.Status);

        builder
            .HasMany(cliente => cliente.Enderecos)
            .WithOne()
            .HasForeignKey(endereco => endereco.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
