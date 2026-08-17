using GestaoClientes.Application.Clientes.Common;
using GestaoClientes.Domain.Repositories;

namespace GestaoClientes.Application.Clientes.Atualizar;

public interface IAtualizarClienteUseCase
{
    Task ExecutarAsync(int id, AtualizarClienteRequest request);
}

public sealed class AtualizarClienteUseCase(
    IClienteRepository repository,
    IUnitOfWork unitOfWork) : ClienteUseCaseBase(repository), IAtualizarClienteUseCase
{
    public async Task ExecutarAsync(int id, AtualizarClienteRequest request)
    {
        var cliente = await ObterCliente(id);
        await VerificarUnicidade(request.Cpf, request.Email, id);

        cliente.AtualizarDados(
            request.Nome,
            request.Cpf,
            request.Email,
            request.Telefone,
            request.DataNascimento);

        await unitOfWork.SalvarAsync();
    }
}
