using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Exceptions;
namespace GestaoClientes.UnitTests;

public class EnderecoTests
{
    [Fact] public void Cep_invalido_e_rejeitado() => Assert.Throws<DomainException>(() => new Endereco("123", "Rua A", "1", null, "Centro", "São Paulo", "SP", true));
    [Fact] public void Estado_invalido_e_rejeitado() => Assert.Throws<DomainException>(() => new Endereco("01001000", "Rua A", "1", null, "Centro", "São Paulo", "S", true));
}
