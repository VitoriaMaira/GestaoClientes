using GestaoClientes.Application.Clientes.Common;
using GestaoClientes.Application.Common.Responses;
using GestaoClientes.Domain.Enums;
using GestaoClientes.Domain.Repositories;
namespace GestaoClientes.Application.Clientes.Listar;
public record ListarClientesQuery(int Pagina=1,int TamanhoPagina=10,string? Nome=null,string? Cpf=null,StatusCliente? Status=null);
public interface IListarClientesUseCase { Task<Paginado<ClienteResponse>> ExecutarAsync(ListarClientesQuery query); }
public sealed class ListarClientesUseCase(IClienteRepository repository) : IListarClientesUseCase
{
    public async Task<Paginado<ClienteResponse>> ExecutarAsync(ListarClientesQuery q) { var pagina=Math.Max(1,q.Pagina);var tamanho=Math.Clamp(q.TamanhoPagina,1,100);var x=await repository.ListarAsync(pagina,tamanho,q.Nome,q.Cpf,q.Status);return new(x.Itens.Select(ClienteMapper.Map).ToList(),pagina,tamanho,x.Total,(int)Math.Ceiling(x.Total/(double)tamanho)); }
}
