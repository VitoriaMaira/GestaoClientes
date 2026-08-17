using GestaoClientes.Application.Clientes.Common;
using GestaoClientes.Domain.Repositories;
namespace GestaoClientes.Application.Clientes.Consultar;

public interface IConsultarClienteUseCase { Task<ClienteResponse> ExecutarAsync(int id); }
public sealed class ConsultarClienteUseCase(IClienteRepository repository) : ClienteUseCaseBase(repository), IConsultarClienteUseCase { public async Task<ClienteResponse> ExecutarAsync(int id) => ClienteMapper.Map(await ObterCliente(id)); }
