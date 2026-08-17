using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Enums;
using GestaoClientes.Domain.Exceptions;

namespace GestaoClientes.UnitTests;

public class ClienteTests
{
    [Fact]
    public void Cliente_valido_inicia_ativo()
    {
        var cliente = CriarCliente();
        Assert.Equal(StatusCliente.Ativo, cliente.Status);
        Assert.Null(cliente.DataAtualizacao);
        Assert.Single(cliente.Enderecos);
    }

    [Fact] public void Nascimento_futuro_e_rejeitado() => Assert.Throws<DomainException>(() => CriarCliente(DateOnly.FromDateTime(DateTime.Today.AddDays(1))));
    [Fact] public void Cliente_sem_endereco_e_rejeitado() => Assert.Throws<DomainException>(() => new Cliente("Maria", "52998224725", "maria@a.com", "1", new DateOnly(1990, 1, 1), null!));

    [Fact]
    public void Novo_principal_substitui_anterior()
    {
        var cliente = CriarCliente();
        cliente.AdicionarEndereco(CriarEndereco(true));
        Assert.Single(cliente.Enderecos, x => x.Principal);
    }

    [Fact]
    public void Nao_remove_unico_endereco()
    {
        var cliente = CriarCliente();
        Assert.Throws<DomainException>(() => cliente.RemoverEndereco(cliente.Enderecos.Single().Id));
    }

    [Fact]
    public void Cliente_pode_ser_inativado_e_ativado()
    {
        var cliente = CriarCliente();
        cliente.Inativar();
        Assert.Equal(StatusCliente.Inativo, cliente.Status);
        Assert.NotNull(cliente.DataAtualizacao);
        cliente.Ativar();
        Assert.Equal(StatusCliente.Ativo, cliente.Status);
    }

    private static Cliente CriarCliente(DateOnly? nascimento = null) => new("Maria", "52998224725", "maria@a.com", "11999999999", nascimento ?? new DateOnly(1990, 1, 1), CriarEndereco());
    private static Endereco CriarEndereco(bool principal = true) => new("01001000", "Rua A", "1", null, "Centro", "São Paulo", "SP", principal);
}
