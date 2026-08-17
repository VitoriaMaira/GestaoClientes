using System.ComponentModel.DataAnnotations;

namespace GestaoClientes.Blazor.Models;

public enum StatusCliente
{
    Ativo,
    Inativo
}

public sealed record ApiResponse<T>(bool Sucesso, string Mensagem, T? Dados);

public sealed record Paginado<T>(
    IReadOnlyList<T> Items,
    int Pagina,
    int TamanhoPagina,
    int TotalItens,
    int TotalPaginas);

public sealed record ClienteModel(
    int Id,
    string Nome,
    string Cpf,
    string Email,
    string Telefone,
    DateOnly DataNascimento,
    DateTime DataCadastro,
    DateTime? DataAtualizacao,
    StatusCliente Status,
    IReadOnlyCollection<EnderecoModel> Enderecos);

public sealed record EnderecoModel(
    int Id,
    string Cep,
    string Logradouro,
    string Numero,
    string? Complemento,
    string Bairro,
    string Cidade,
    string Estado,
    bool Principal);

public sealed class ClienteFormModel
{
    [Required(ErrorMessage = "Informe o nome.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o CPF.")]
    public string Cpf { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o e-mail.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o telefone.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a data de nascimento.")]
    public DateOnly? DataNascimento { get; set; }

    [Required(ErrorMessage = "Informe o CEP.")]
    public string EnderecoCep { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o logradouro.")]
    public string EnderecoLogradouro { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o número.")]
    public string EnderecoNumero { get; set; } = string.Empty;

    public string? EnderecoComplemento { get; set; }

    [Required(ErrorMessage = "Informe o bairro.")]
    public string EnderecoBairro { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a cidade.")]
    public string EnderecoCidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o estado.")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "Use a sigla do estado com 2 letras.")]
    public string EnderecoEstado { get; set; } = string.Empty;
}

public sealed class EnderecoFormModel
{
    [Required(ErrorMessage = "Informe o CEP.")]
    public string Cep { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o logradouro.")]
    public string Logradouro { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o número.")]
    public string Numero { get; set; } = string.Empty;

    public string? Complemento { get; set; }

    [Required(ErrorMessage = "Informe o bairro.")]
    public string Bairro { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a cidade.")]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o estado.")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "Use a sigla do estado com 2 letras.")]
    public string Estado { get; set; } = string.Empty;

    public bool Principal { get; set; }
}

public sealed record CriarClienteRequest(
    string Nome,
    string Cpf,
    string Email,
    string Telefone,
    DateOnly DataNascimento,
    EnderecoRequest Endereco);

public sealed record AtualizarClienteRequest(
    string Nome,
    string Cpf,
    string Email,
    string Telefone,
    DateOnly DataNascimento);

public sealed record EnderecoRequest(
    string Cep,
    string Logradouro,
    string Numero,
    string? Complemento,
    string Bairro,
    string Cidade,
    string Estado,
    bool Principal);
