namespace GestaoClientes.Domain;

public enum StatusCliente { Ativo, Inativo }
public sealed class DomainException(string message) : Exception(message);

public sealed class Endereco
{
    private Endereco() { }
    public Endereco(string cep, string logradouro, string numero, string? complemento, string bairro, string cidade, string estado, bool principal)
    { Cep = Obrigatorio(cep, "CEP"); Logradouro = Obrigatorio(logradouro, "Logradouro"); Numero = Obrigatorio(numero, "Número"); Complemento = complemento; Bairro = Obrigatorio(bairro, "Bairro"); Cidade = Obrigatorio(cidade, "Cidade"); Estado = Obrigatorio(estado, "Estado"); Principal = principal; }
    public int Id { get; private set; } public int ClienteId { get; private set; }
    public string Cep { get; private set; } = null!; public string Logradouro { get; private set; } = null!; public string Numero { get; private set; } = null!; public string? Complemento { get; private set; }
    public string Bairro { get; private set; } = null!; public string Cidade { get; private set; } = null!; public string Estado { get; private set; } = null!; public bool Principal { get; private set; }
    public void Atualizar(string cep,string logradouro,string numero,string? complemento,string bairro,string cidade,string estado,bool principal) { Cep=Obrigatorio(cep,"CEP"); Logradouro=Obrigatorio(logradouro,"Logradouro"); Numero=Obrigatorio(numero,"Número"); Complemento=complemento; Bairro=Obrigatorio(bairro,"Bairro"); Cidade=Obrigatorio(cidade,"Cidade"); Estado=Obrigatorio(estado,"Estado"); Principal=principal; }
    public void DefinirPrincipal(bool valor) => Principal = valor;
    internal static string Obrigatorio(string valor,string campo) => string.IsNullOrWhiteSpace(valor) ? throw new DomainException($"{campo} é obrigatório.") : valor.Trim();
}

public sealed class Cliente
{
    private Cliente() { }
    public Cliente(string nome,string cpf,string email,string telefone,DateOnly dataNascimento, Endereco endereco)
    { AtualizarDados(nome,cpf,email,telefone,dataNascimento); DataCadastro=DateTime.UtcNow; Status=StatusCliente.Ativo; endereco.DefinirPrincipal(true); _enderecos.Add(endereco); }
    public int Id { get; private set; } public string Nome { get; private set; } = null!; public string Cpf { get; private set; } = null!; public string Email { get; private set; } = null!; public string Telefone { get; private set; } = null!; public DateOnly DataNascimento { get; private set; }
    public DateTime DataCadastro { get; private set; } public DateTime? DataAtualizacao { get; private set; } public StatusCliente Status { get; private set; }
    private readonly List<Endereco> _enderecos=[]; public IReadOnlyCollection<Endereco> Enderecos => _enderecos;
    public void AtualizarDados(string nome,string cpf,string email,string telefone,DateOnly dataNascimento) { Nome=Endereco.Obrigatorio(nome,"Nome"); Cpf=ValidarCpf(cpf); if(!System.Net.Mail.MailAddress.TryCreate(email,out _)) throw new DomainException("E-mail inválido."); Email=email.Trim(); Telefone=Endereco.Obrigatorio(telefone,"Telefone"); if(dataNascimento>DateOnly.FromDateTime(DateTime.Today)) throw new DomainException("Data de nascimento não pode ser futura."); DataNascimento=dataNascimento; DataAtualizacao=DateTime.UtcNow; }
    public void Inativar()=>Status=StatusCliente.Inativo; public void Ativar()=>Status=StatusCliente.Ativo;
    public void AdicionarEndereco(Endereco endereco) { if(endereco.Principal) foreach(var item in _enderecos)item.DefinirPrincipal(false); else if(!_enderecos.Any(x=>x.Principal)) endereco.DefinirPrincipal(true); _enderecos.Add(endereco); }
    public Endereco ObterEndereco(int id)=>_enderecos.SingleOrDefault(x=>x.Id==id)??throw new DomainException("Endereço não encontrado.");
    public void RemoverEndereco(int id) { if(_enderecos.Count==1) throw new DomainException("Não é permitido excluir o único endereço do cliente."); _enderecos.Remove(ObterEndereco(id)); if(!_enderecos.Any(x=>x.Principal)) _enderecos[0].DefinirPrincipal(true); }
    public void DefinirEnderecoPrincipal(int id) { foreach(var item in _enderecos)item.DefinirPrincipal(item.Id==id); }
    private static string ValidarCpf(string cpf) { var n=new string(cpf.Where(char.IsDigit).ToArray()); if(n.Length!=11||n.Distinct().Count()==1) throw new DomainException("CPF inválido."); for(int p=9;p<=10;p++){var s=0;for(int i=0;i<p;i++)s+=(n[i]-'0')*(p+1-i);var d=(s*10)%11; if(d==10)d=0;if(d!=n[p]-'0')throw new DomainException("CPF inválido.");} return n; }
}

public interface IClienteRepository { Task<bool> CpfExisteAsync(string cpf,int? ignorarId=null); Task<bool> EmailExisteAsync(string email,int? ignorarId=null); Task<Cliente?> ObterAsync(int id); Task<(IReadOnlyList<Cliente> Itens,int Total)> ListarAsync(int pagina,int tamanho,string? nome,string? cpf,StatusCliente? status); Task AdicionarAsync(Cliente cliente); Task SalvarAsync(); }
