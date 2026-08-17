using GestaoClientes.Application.Clientes.Common;
using GestaoClientes.Domain.Repositories;
namespace GestaoClientes.Application.Clientes.Atualizar;

public interface IAtualizarClienteUseCase { Task ExecutarAsync(int id, AtualizarClienteRequest request); }
public sealed class AtualizarClienteUseCase(IClienteRepository repository, IUnitOfWork unitOfWork) : ClienteUseCaseBase(repository), IAtualizarClienteUseCase
{
    public async Task ExecutarAsync(int id, AtualizarClienteRequest r) { var cliente = await ObterCliente(id); await VerificarUnicidade(r.Cpf, r.Email, id); cliente.AtualizarDados(r.Nome, r.Cpf, r.Email, r.Telefone, r.DataNascimento); await unitOfWork.SalvarAsync(); }
}
