using GestaoClientes.Application.Clientes.Common;
using GestaoClientes.Domain.Repositories;
namespace GestaoClientes.Application.Enderecos;

public interface IGerenciarEnderecosUseCase { Task<IReadOnlyCollection<EnderecoResponse>> ListarAsync(int clienteId); Task AdicionarAsync(int clienteId, EnderecoRequest request); Task AtualizarAsync(int clienteId, int enderecoId, EnderecoRequest request); Task RemoverAsync(int clienteId, int enderecoId); Task DefinirPrincipalAsync(int clienteId, int enderecoId); }
public sealed class GerenciarEnderecosUseCase(IClienteRepository repository, IUnitOfWork unitOfWork) : ClienteUseCaseBase(repository), IGerenciarEnderecosUseCase
{
    public async Task<IReadOnlyCollection<EnderecoResponse>> ListarAsync(int id) => (await ObterCliente(id)).Enderecos.Select(ClienteMapper.Map).ToList();
    public async Task AdicionarAsync(int id, EnderecoRequest r) { (await ObterCliente(id)).AdicionarEndereco(ClienteMapper.Map(r)); await unitOfWork.SalvarAsync(); }
    public async Task AtualizarAsync(int id, int enderecoId, EnderecoRequest r) { var c = await ObterCliente(id); var endereco = c.ObterEndereco(enderecoId); var eraPrincipal = endereco.Principal; endereco.Atualizar(r.Cep, r.Logradouro, r.Numero, r.Complemento, r.Bairro, r.Cidade, r.Estado, r.Principal); if (r.Principal || eraPrincipal) c.DefinirEnderecoPrincipal(enderecoId); await unitOfWork.SalvarAsync(); }
    public async Task RemoverAsync(int id, int enderecoId) { (await ObterCliente(id)).RemoverEndereco(enderecoId); await unitOfWork.SalvarAsync(); }
    public async Task DefinirPrincipalAsync(int id, int enderecoId) { (await ObterCliente(id)).DefinirEnderecoPrincipal(enderecoId); await unitOfWork.SalvarAsync(); }
}
