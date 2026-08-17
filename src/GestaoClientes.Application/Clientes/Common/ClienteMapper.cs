using GestaoClientes.Domain.Entities;
namespace GestaoClientes.Application.Clientes.Common;
internal static class ClienteMapper
{
    public static ClienteResponse Map(Cliente c)=>new(c.Id,c.Nome,c.Cpf,c.Email,c.Telefone,c.DataNascimento,c.DataCadastro,c.DataAtualizacao,c.Status,c.Enderecos.Select(Map).ToList());
    public static EnderecoResponse Map(Endereco e)=>new(e.Id,e.Cep,e.Logradouro,e.Numero,e.Complemento,e.Bairro,e.Cidade,e.Estado,e.Principal);
    public static Endereco Map(EnderecoRequest r)=>new(r.Cep,r.Logradouro,r.Numero,r.Complemento,r.Bairro,r.Cidade,r.Estado,r.Principal);
}
