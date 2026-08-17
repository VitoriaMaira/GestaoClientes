using GestaoClientes.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestaoClientes.Application.Clientes.Common;

public record EnderecoRequest(
    [Required] string Cep,
    [Required] string Logradouro,
    [Required] string Numero,
    string? Complemento,
    [Required] string Bairro,
    [Required] string Cidade,
    [Required] string Estado,
    bool Principal);

public record AdicionarEnderecoRequest(
    [Required] string Cep,
    [Required] string Logradouro,
    [Required] string Numero,
    string? Complemento,
    [Required] string Bairro,
    [Required] string Cidade,
    [Required] string Estado,
    bool Principal);

public record AtualizarEnderecoRequest(
    [Required] string Cep,
    [Required] string Logradouro,
    [Required] string Numero,
    string? Complemento,
    [Required] string Bairro,
    [Required] string Cidade,
    [Required] string Estado,
    bool Principal);

public record CriarClienteRequest(
    [Required] string Nome,
    [Required] string Cpf,
    [Required, EmailAddress] string Email,
    [Required] string Telefone,
    DateOnly DataNascimento,
    [Required] EnderecoRequest Endereco);

public record AtualizarClienteRequest(
    [Required] string Nome,
    [Required] string Cpf,
    [Required, EmailAddress] string Email,
    [Required] string Telefone,
    DateOnly DataNascimento);

public record ClienteResponse(
    int Id,
    string Nome,
    string Cpf,
    string Email,
    string Telefone,
    DateOnly DataNascimento,
    DateTime DataCadastro,
    DateTime? DataAtualizacao,
    StatusCliente Status,
    IReadOnlyCollection<EnderecoResponse> Enderecos);

public record EnderecoResponse(
    int Id,
    string Cep,
    string Logradouro,
    string Numero,
    string? Complemento,
    string Bairro,
    string Cidade,
    string Estado,
    bool Principal);
