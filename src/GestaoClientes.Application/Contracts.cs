using GestaoClientes.Domain;
using System.ComponentModel.DataAnnotations;
namespace GestaoClientes.Application;
public record EnderecoRequest([property: Required] string Cep,[property: Required] string Logradouro,[property: Required] string Numero,string? Complemento,[property: Required] string Bairro,[property: Required] string Cidade,[property: Required] string Estado,bool Principal);
public record CriarClienteRequest([property: Required] string Nome,[property: Required] string Cpf,[property: Required, EmailAddress] string Email,[property: Required] string Telefone,DateOnly DataNascimento,[property: Required] EnderecoRequest Endereco);
public record AtualizarClienteRequest([property: Required] string Nome,[property: Required] string Cpf,[property: Required, EmailAddress] string Email,[property: Required] string Telefone,DateOnly DataNascimento);
public record ClienteResponse(int Id,string Nome,string Cpf,string Email,string Telefone,DateOnly DataNascimento,DateTime DataCadastro,DateTime? DataAtualizacao,StatusCliente Status,IReadOnlyCollection<EnderecoResponse> Enderecos);
public record EnderecoResponse(int Id,string Cep,string Logradouro,string Numero,string? Complemento,string Bairro,string Cidade,string Estado,bool Principal);
public record Paginado<T>(IReadOnlyList<T> Items,int Pagina,int TamanhoPagina,int TotalItens,int TotalPaginas);
public sealed class ClienteService(IClienteRepository repo)
{
 public async Task<ClienteResponse> CriarAsync(CriarClienteRequest r){ await VerificarUnicos(r.Cpf,r.Email); var c=new Cliente(r.Nome,r.Cpf,r.Email,r.Telefone,r.DataNascimento,End(r.Endereco)); await repo.AdicionarAsync(c); await repo.SalvarAsync(); return Map(c); }
 public async Task<Paginado<ClienteResponse>> ListarAsync(int pagina,int tamanho,string? nome,string? cpf,StatusCliente? status){pagina=Math.Max(1,pagina);tamanho=Math.Clamp(tamanho,1,100);var x=await repo.ListarAsync(pagina,tamanho,nome,cpf,status);return new(x.Itens.Select(Map).ToList(),pagina,tamanho,x.Total,(int)Math.Ceiling(x.Total/(double)tamanho));}
 public async Task<ClienteResponse> ObterAsync(int id)=>Map(await Obter(id));
 public async Task AtualizarAsync(int id,AtualizarClienteRequest r){var c=await Obter(id);await VerificarUnicos(r.Cpf,r.Email,id);c.AtualizarDados(r.Nome,r.Cpf,r.Email,r.Telefone,r.DataNascimento);await repo.SalvarAsync();}
 public async Task InativarAsync(int id){(await Obter(id)).Inativar();await repo.SalvarAsync();} public async Task AtivarAsync(int id){(await Obter(id)).Ativar();await repo.SalvarAsync();}
 public async Task<IReadOnlyCollection<EnderecoResponse>> EnderecosAsync(int id)=>(await Obter(id)).Enderecos.Select(Map).ToList();
 public async Task AdicionarEnderecoAsync(int id,EnderecoRequest r){var c=await Obter(id);c.AdicionarEndereco(End(r));await repo.SalvarAsync();}
 public async Task AtualizarEnderecoAsync(int id,int enderecoId,EnderecoRequest r){var c=await Obter(id);c.ObterEndereco(enderecoId).Atualizar(r.Cep,r.Logradouro,r.Numero,r.Complemento,r.Bairro,r.Cidade,r.Estado,r.Principal);if(r.Principal)c.DefinirEnderecoPrincipal(enderecoId);await repo.SalvarAsync();}
 public async Task RemoverEnderecoAsync(int id,int enderecoId){(await Obter(id)).RemoverEndereco(enderecoId);await repo.SalvarAsync();} public async Task PrincipalAsync(int id,int enderecoId){(await Obter(id)).DefinirEnderecoPrincipal(enderecoId);await repo.SalvarAsync();}
 async Task<Cliente> Obter(int id)=>await repo.ObterAsync(id)??throw new KeyNotFoundException("Cliente não encontrado."); async Task VerificarUnicos(string cpf,string email,int? id=null){if(await repo.CpfExisteAsync(new string(cpf.Where(char.IsDigit).ToArray()),id))throw new InvalidOperationException("CPF já cadastrado.");if(await repo.EmailExisteAsync(email.Trim(),id))throw new InvalidOperationException("E-mail já cadastrado.");} static Endereco End(EnderecoRequest r)=>new(r.Cep,r.Logradouro,r.Numero,r.Complemento,r.Bairro,r.Cidade,r.Estado,r.Principal); static EnderecoResponse Map(Endereco e)=>new(e.Id,e.Cep,e.Logradouro,e.Numero,e.Complemento,e.Bairro,e.Cidade,e.Estado,e.Principal); static ClienteResponse Map(Cliente c)=>new(c.Id,c.Nome,c.Cpf,c.Email,c.Telefone,c.DataNascimento,c.DataCadastro,c.DataAtualizacao,c.Status,c.Enderecos.Select(Map).ToList());
}
