using GestaoClientes.Application.Clientes.Common;
using GestaoClientes.Domain.Repositories;
namespace GestaoClientes.Application.Clientes.AlterarStatus;

public interface IAlterarStatusClienteUseCase { Task InativarAsync(int id); Task AtivarAsync(int id); }
public sealed class AlterarStatusClienteUseCase(IClienteRepository repository, IUnitOfWork unitOfWork) : ClienteUseCaseBase(repository), IAlterarStatusClienteUseCase
{
    public async Task InativarAsync(int id) { (await ObterCliente(id)).Inativar(); await unitOfWork.SalvarAsync(); }
    public async Task AtivarAsync(int id) { (await ObterCliente(id)).Ativar(); await unitOfWork.SalvarAsync(); }
}
