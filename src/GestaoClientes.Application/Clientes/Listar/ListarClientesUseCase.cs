using GestaoClientes.Application.Clientes.Common;
using GestaoClientes.Application.Common.Responses;
using GestaoClientes.Domain.Enums;
using GestaoClientes.Domain.Repositories;

namespace GestaoClientes.Application.Clientes.Listar;

public record ListarClientesQuery(
    int Pagina = 1,
    int TamanhoPagina = 10,
    string? Nome = null,
    string? Cpf = null,
    StatusCliente? Status = null);

public interface IListarClientesUseCase
{
    Task<Paginado<ClienteResponse>> ExecutarAsync(ListarClientesQuery query);
}

public sealed class ListarClientesUseCase(IClienteRepository repository) : IListarClientesUseCase
{
    public async Task<Paginado<ClienteResponse>> ExecutarAsync(ListarClientesQuery query)
    {
        var pagina = Math.Max(1, query.Pagina);
        var tamanho = Math.Clamp(query.TamanhoPagina, 1, 100);
        var resultado = await repository.ListarAsync(
            pagina,
            tamanho,
            query.Nome,
            query.Cpf,
            query.Status);

        return new Paginado<ClienteResponse>(
            resultado.Itens.Select(ClienteMapper.Map).ToList(),
            pagina,
            tamanho,
            resultado.Total,
            (int)Math.Ceiling(resultado.Total / (double)tamanho));
    }
}
