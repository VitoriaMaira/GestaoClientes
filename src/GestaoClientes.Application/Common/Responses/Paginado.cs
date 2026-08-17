namespace GestaoClientes.Application.Common.Responses;

public sealed record Paginado<T>(IReadOnlyList<T> Items, int Pagina, int TamanhoPagina, int TotalItens, int TotalPaginas);
