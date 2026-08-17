using GestaoClientes.Domain.Entities;

namespace GestaoClientes.Application.Clientes.Common;

internal static class ClienteMapper
{
    public static ClienteResponse Map(Cliente cliente) => new(
        cliente.Id,
        cliente.Nome,
        cliente.Cpf,
        cliente.Email,
        cliente.Telefone,
        cliente.DataNascimento,
        cliente.DataCadastro,
        cliente.DataAtualizacao,
        cliente.Status,
        cliente.Enderecos.Select(Map).ToList());

    public static EnderecoResponse Map(Endereco endereco) => new(
        endereco.Id,
        endereco.Cep,
        endereco.Logradouro,
        endereco.Numero,
        endereco.Complemento,
        endereco.Bairro,
        endereco.Cidade,
        endereco.Estado,
        endereco.Principal);

    public static Endereco Map(EnderecoRequest request) => new(
        request.Cep,
        request.Logradouro,
        request.Numero,
        request.Complemento,
        request.Bairro,
        request.Cidade,
        request.Estado,
        request.Principal);

    public static Endereco Map(AdicionarEnderecoRequest request) => new(
        request.Cep,
        request.Logradouro,
        request.Numero,
        request.Complemento,
        request.Bairro,
        request.Cidade,
        request.Estado,
        request.Principal);
}
