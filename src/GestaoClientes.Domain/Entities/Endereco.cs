using GestaoClientes.Domain.Exceptions;

namespace GestaoClientes.Domain.Entities;

public sealed class Endereco
{
    private Endereco() { }

    public Endereco(string cep, string logradouro, string numero, string? complemento, string bairro, string cidade, string estado, bool principal)
    {
        Atualizar(cep, logradouro, numero, complemento, bairro, cidade, estado, principal);
    }

    public int Id { get; private set; }
    public int ClienteId { get; private set; }
    public string Cep { get; private set; } = null!;
    public string Logradouro { get; private set; } = null!;
    public string Numero { get; private set; } = null!;
    public string? Complemento { get; private set; }
    public string Bairro { get; private set; } = null!;
    public string Cidade { get; private set; } = null!;
    public string Estado { get; private set; } = null!;
    public bool Principal { get; private set; }

    public void Atualizar(string cep, string logradouro, string numero, string? complemento, string bairro, string cidade, string estado, bool principal)
    {
        Cep = SomenteDigitos(cep, "CEP", 8);
        Logradouro = Obrigatorio(logradouro, "Logradouro");
        Numero = Obrigatorio(numero, "Número");
        Complemento = complemento?.Trim();
        Bairro = Obrigatorio(bairro, "Bairro");
        Cidade = Obrigatorio(cidade, "Cidade");
        Estado = Obrigatorio(estado, "Estado").ToUpperInvariant();
        if (Estado.Length != 2) throw new DomainException("Estado deve possuir 2 caracteres.");
        Principal = principal;
    }

    public void DefinirPrincipal(bool valor) => Principal = valor;

    internal static string Obrigatorio(string valor, string campo) =>
        string.IsNullOrWhiteSpace(valor) ? throw new DomainException($"{campo} é obrigatório.") : valor.Trim();

    private static string SomenteDigitos(string valor, string campo, int tamanho)
    {
        var numero = new string(Obrigatorio(valor, campo).Where(char.IsDigit).ToArray());
        return numero.Length == tamanho ? numero : throw new DomainException($"{campo} inválido.");
    }
}
