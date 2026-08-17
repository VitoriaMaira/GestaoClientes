namespace GestaoClientes.Application.Common.Responses;
public sealed record ApiResponse<T>(bool Sucesso, string Mensagem, T? Dados)
{
    public static ApiResponse<T> Ok(string mensagem, T? dados = default) => new(true, mensagem, dados);
    public static ApiResponse<T> Erro(string mensagem) => new(false, mensagem, default);
}
