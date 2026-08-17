using GestaoClientes.Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace GestaoClientes.Application.Clientes.Common;
public record EnderecoRequest([property: Required] string Cep,[property: Required] string Logradouro,[property: Required] string Numero,string? Complemento,[property: Required] string Bairro,[property: Required] string Cidade,[property: Required] string Estado,bool Principal);
public record CriarClienteRequest([property: Required] string Nome,[property: Required] string Cpf,[property: Required, EmailAddress] string Email,[property: Required] string Telefone,DateOnly DataNascimento,[property: Required] EnderecoRequest Endereco);
public record AtualizarClienteRequest([property: Required] string Nome,[property: Required] string Cpf,[property: Required, EmailAddress] string Email,[property: Required] string Telefone,DateOnly DataNascimento);
public record ClienteResponse(int Id,string Nome,string Cpf,string Email,string Telefone,DateOnly DataNascimento,DateTime DataCadastro,DateTime? DataAtualizacao,StatusCliente Status,IReadOnlyCollection<EnderecoResponse> Enderecos);
public record EnderecoResponse(int Id,string Cep,string Logradouro,string Numero,string? Complemento,string Bairro,string Cidade,string Estado,bool Principal);
