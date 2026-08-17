using GestaoClientes.Domain.Exceptions;

namespace GestaoClientes.Domain.ValueObjects;

public readonly record struct Cpf
{
    public Cpf(string valor)
    {
        Valor = new string((valor ?? string.Empty).Where(char.IsDigit).ToArray());
        if (Valor.Length != 11 || Valor.Distinct().Count() == 1)
            throw new DomainException("CPF inválido.");

        for (var posicao = 9; posicao <= 10; posicao++)
        {
            var soma = 0;
            for (var i = 0; i < posicao; i++)
                soma += (Valor[i] - '0') * (posicao + 1 - i);

            var digito = (soma * 10) % 11;
            if (digito == 10) digito = 0;
            if (digito != Valor[posicao] - '0') throw new DomainException("CPF inválido.");
        }
    }

    public string Valor { get; }
    public override string ToString() => Valor;
}
