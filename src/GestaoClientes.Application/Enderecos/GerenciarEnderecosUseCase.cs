using GestaoClientes.Application.Clientes.Common;
using GestaoClientes.Domain.Repositories;

namespace GestaoClientes.Application.Enderecos;

public interface IGerenciarEnderecosUseCase
{
    Task<IReadOnlyCollection<EnderecoResponse>> ListarAsync(int clienteId);
    Task AdicionarAsync(int clienteId, AdicionarEnderecoRequest request);
    Task AtualizarAsync(int clienteId, int enderecoId, AtualizarEnderecoRequest request);
    Task RemoverAsync(int clienteId, int enderecoId);
    Task DefinirPrincipalAsync(int clienteId, int enderecoId);
}

public sealed class GerenciarEnderecosUseCase(
    IClienteRepository repository,
    IUnitOfWork unitOfWork) : ClienteUseCaseBase(repository), IGerenciarEnderecosUseCase
{
    public async Task<IReadOnlyCollection<EnderecoResponse>> ListarAsync(int clienteId)
    {
        var cliente = await ObterCliente(clienteId);
        return cliente.Enderecos.Select(ClienteMapper.Map).ToList();
    }

    public async Task AdicionarAsync(int clienteId, AdicionarEnderecoRequest request)
    {
        var cliente = await ObterCliente(clienteId);
        cliente.AdicionarEndereco(ClienteMapper.Map(request));
        await unitOfWork.SalvarAsync();
    }

    public async Task AtualizarAsync(
        int clienteId,
        int enderecoId,
        AtualizarEnderecoRequest request)
    {
        var cliente = await ObterCliente(clienteId);
        var endereco = cliente.ObterEndereco(enderecoId);
        var eraPrincipal = endereco.Principal;

        endereco.Atualizar(
            request.Cep,
            request.Logradouro,
            request.Numero,
            request.Complemento,
            request.Bairro,
            request.Cidade,
            request.Estado,
            request.Principal);

        if (request.Principal || eraPrincipal)
        {
            cliente.DefinirEnderecoPrincipal(enderecoId);
        }

        await unitOfWork.SalvarAsync();
    }

    public async Task RemoverAsync(int clienteId, int enderecoId)
    {
        var cliente = await ObterCliente(clienteId);
        cliente.RemoverEndereco(enderecoId);
        await unitOfWork.SalvarAsync();
    }

    public async Task DefinirPrincipalAsync(int clienteId, int enderecoId)
    {
        var cliente = await ObterCliente(clienteId);
        cliente.DefinirEnderecoPrincipal(enderecoId);
        await unitOfWork.SalvarAsync();
    }
}
