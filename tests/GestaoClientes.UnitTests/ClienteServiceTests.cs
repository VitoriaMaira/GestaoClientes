using GestaoClientes.Application;
using GestaoClientes.Domain;
namespace GestaoClientes.UnitTests;
public class ClienteServiceTests
{
    [Fact] public async Task Nao_permite_cpf_duplicado()
    {
        var repo = new RepositorioFalso { CpfExiste = true };
        await Assert.ThrowsAsync<InvalidOperationException>(() => new ClienteService(repo).CriarAsync(Requisicao()));
    }
    [Fact] public async Task Nao_permite_email_duplicado()
    {
        var repo = new RepositorioFalso { EmailExiste = true };
        await Assert.ThrowsAsync<InvalidOperationException>(() => new ClienteService(repo).CriarAsync(Requisicao()));
    }
    static CriarClienteRequest Requisicao() => new("Maria", "52998224725", "maria@exemplo.com", "11999999999", new DateOnly(1990, 1, 1), new("01001000", "Rua A", "1", null, "Centro", "São Paulo", "SP", true));
    private sealed class RepositorioFalso : IClienteRepository
    {
        public bool CpfExiste { get; init; } public bool EmailExiste { get; init; }
        public Task<bool> CpfExisteAsync(string cpf, int? ignorarId = null) => Task.FromResult(CpfExiste);
        public Task<bool> EmailExisteAsync(string email, int? ignorarId = null) => Task.FromResult(EmailExiste);
        public Task<Cliente?> ObterAsync(int id) => Task.FromResult<Cliente?>(null);
        public Task<(IReadOnlyList<Cliente> Itens, int Total)> ListarAsync(int pagina, int tamanho, string? nome, string? cpf, StatusCliente? status) => Task.FromResult<(IReadOnlyList<Cliente>, int)>(([], 0));
        public Task AdicionarAsync(Cliente cliente) => Task.CompletedTask; public Task SalvarAsync() => Task.CompletedTask;
    }
}
