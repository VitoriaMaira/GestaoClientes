using GestaoClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoClientes.Infrastructure.DataAccess.Mappings;

public sealed class EnderecoMapping : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Enderecos");
        builder.HasKey(endereco => endereco.Id);

        builder.Property(endereco => endereco.Cep).HasMaxLength(8).IsRequired();
        builder.Property(endereco => endereco.Logradouro).HasMaxLength(200).IsRequired();
        builder.Property(endereco => endereco.Numero).HasMaxLength(20).IsRequired();
        builder.Property(endereco => endereco.Complemento).HasMaxLength(100);
        builder.Property(endereco => endereco.Bairro).HasMaxLength(100).IsRequired();
        builder.Property(endereco => endereco.Cidade).HasMaxLength(100).IsRequired();
        builder.Property(endereco => endereco.Estado).HasMaxLength(2).IsRequired();
    }
}
