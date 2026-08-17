using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Exceptions;

namespace GestaoClientes.UnitTests;

public class EnderecoTests
{
    [Fact]
    public void Cep_invalido_e_rejeitado()
    {
        Assert.Throws<DomainException>(() => CriarEndereco(cep: "123"));
    }

    [Fact]
    public void Estado_invalido_e_rejeitado()
    {
        Assert.Throws<DomainException>(() => CriarEndereco(estado: "S"));
    }

    private static Endereco CriarEndereco(
        string cep = "01001000",
        string estado = "SP")
    {
        return new Endereco(
            cep,
            "Rua A",
            "1",
            null,
            "Centro",
            "São Paulo",
            estado,
            true);
    }
}
