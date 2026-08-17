using GestaoClientes.Application.Common.Exceptions;
using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Repositories;
namespace GestaoClientes.Application.Clientes.Common;

public abstract class ClienteUseCaseBase(IClienteRepository repository)
{
    protected IClienteRepository Repository { get; } = repository;
    protected async Task<Cliente> ObterCliente(int id) => await Repository.ObterAsync(id) ?? throw new NotFoundException("Cliente não encontrado.");
    protected async Task VerificarUnicidade(string cpf, string email, int? id = null)
    {
        var normalizado = new string(cpf.Where(char.IsDigit).ToArray());
        if (await Repository.CpfExisteAsync(normalizado, id)) throw new ConflictException("CPF já cadastrado.");
        if (await Repository.EmailExisteAsync(email.Trim(), id)) throw new ConflictException("E-mail já cadastrado.");
    }
}
