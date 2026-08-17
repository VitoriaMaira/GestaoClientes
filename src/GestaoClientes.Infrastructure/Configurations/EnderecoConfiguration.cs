using GestaoClientes.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GestaoClientes.Infrastructure.Configurations;
public sealed class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> e)
    {
        e.ToTable("Enderecos"); e.HasKey(x => x.Id);
        e.Property(x => x.Cep).HasMaxLength(8).IsRequired(); e.Property(x => x.Logradouro).HasMaxLength(200).IsRequired();
        e.Property(x => x.Numero).HasMaxLength(20).IsRequired(); e.Property(x => x.Bairro).HasMaxLength(100).IsRequired();
        e.Property(x => x.Cidade).HasMaxLength(100).IsRequired(); e.Property(x => x.Estado).HasMaxLength(2).IsRequired();
    }
}
