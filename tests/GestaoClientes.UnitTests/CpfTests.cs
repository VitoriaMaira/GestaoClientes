using GestaoClientes.Domain.Exceptions;
using GestaoClientes.Domain.ValueObjects;
namespace GestaoClientes.UnitTests;
public class CpfTests
{
    [Fact] public void Cpf_formatado_e_normalizado()=>Assert.Equal("52998224725",new Cpf("529.982.247-25").Valor);
    [Theory][InlineData("")][InlineData("11111111111")][InlineData("12345678900")] public void Cpf_invalido_e_rejeitado(string valor)=>Assert.Throws<DomainException>(()=>new Cpf(valor));
}
