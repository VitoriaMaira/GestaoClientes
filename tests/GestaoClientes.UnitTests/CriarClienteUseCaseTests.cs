using GestaoClientes.Application.Clientes.Common;
using GestaoClientes.Application.Clientes.Criar;
using GestaoClientes.Application.Common.Exceptions;
using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Enums;
using GestaoClientes.Domain.Repositories;

namespace GestaoClientes.UnitTests;

public class CriarClienteUseCaseTests
{
    [Fact] public async Task Nao_permite_cpf_duplicado() { var repo = new RepositorioFalso { CpfExiste = true }; await Assert.ThrowsAsync<ConflictException>(() => new CriarClienteUseCase(repo, repo).ExecutarAsync(Requisicao())); }
    [Fact] public async Task Nao_permite_email_duplicado() { var repo = new RepositorioFalso { EmailExiste = true }; await Assert.ThrowsAsync<ConflictException>(() => new CriarClienteUseCase(repo, repo).ExecutarAsync(Requisicao())); }

    [Fact]
    public async Task Cliente_valido_pode_ser_criado()
    {
        var repo = new RepositorioFalso();
        var response = await new CriarClienteUseCase(repo, repo).ExecutarAsync(Requisicao());
        Assert.Equal("Maria", response.Nome);
        Assert.True(repo.Salvou);
    }

    private static CriarClienteRequest Requisicao() => new("Maria", "52998224725", "maria@exemplo.com", "11999999999", new DateOnly(1990, 1, 1), new("01001000", "Rua A", "1", null, "Centro", "São Paulo", "SP", true));

    private sealed class RepositorioFalso : IClienteRepository, IUnitOfWork
    {
        public bool CpfExiste { get; init; }
        public bool EmailExiste { get; init; }
        public bool Salvou { get; private set; }
        public Task<bool> CpfExisteAsync(string cpf, int? ignorarId = null) => Task.FromResult(CpfExiste);
        public Task<bool> EmailExisteAsync(string email, int? ignorarId = null) => Task.FromResult(EmailExiste);
        public Task<Cliente?> ObterAsync(int id) => Task.FromResult<Cliente?>(null);
        public Task<(IReadOnlyList<Cliente> Itens, int Total)> ListarAsync(int pagina, int tamanho, string? nome, string? cpf, StatusCliente? status) => Task.FromResult<(IReadOnlyList<Cliente>, int)>(([], 0));
        public Task AdicionarAsync(Cliente cliente) => Task.CompletedTask;
        public Task SalvarAsync() { Salvou = true; return Task.CompletedTask; }
    }
}
