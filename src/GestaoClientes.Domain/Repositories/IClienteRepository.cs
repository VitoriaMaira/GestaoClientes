using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Enums;

namespace GestaoClientes.Domain.Repositories;

public interface IClienteRepository
{
    Task<bool> CpfExisteAsync(string cpf, int? ignorarId = null);
    Task<bool> EmailExisteAsync(string email, int? ignorarId = null);
    Task<Cliente?> ObterAsync(int id);
    Task<(IReadOnlyList<Cliente> Itens, int Total)> ListarAsync(int pagina, int tamanho, string? nome, string? cpf, StatusCliente? status);
    Task AdicionarAsync(Cliente cliente);
}
