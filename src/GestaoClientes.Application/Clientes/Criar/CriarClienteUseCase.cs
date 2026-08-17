using GestaoClientes.Application.Clientes.Common;
using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Repositories;
namespace GestaoClientes.Application.Clientes.Criar;
public interface ICriarClienteUseCase { Task<ClienteResponse> ExecutarAsync(CriarClienteRequest request); }
public sealed class CriarClienteUseCase(IClienteRepository repository,IUnitOfWork unitOfWork) : ClienteUseCaseBase(repository), ICriarClienteUseCase
{
    public async Task<ClienteResponse> ExecutarAsync(CriarClienteRequest request) { await VerificarUnicidade(request.Cpf,request.Email); var cliente=new Cliente(request.Nome,request.Cpf,request.Email,request.Telefone,request.DataNascimento,ClienteMapper.Map(request.Endereco)); await Repository.AdicionarAsync(cliente); await unitOfWork.SalvarAsync(); return ClienteMapper.Map(cliente); }
}
