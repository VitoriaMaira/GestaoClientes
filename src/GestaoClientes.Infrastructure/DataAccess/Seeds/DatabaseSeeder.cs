using GestaoClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoClientes.Infrastructure.DataAccess.Seeds;

public static class DatabaseSeeder
{
    public static async Task SemearAsync(GestaoClientesDbContext db)
    {
        await db.Database.MigrateAsync();

        if (await db.Clientes.AnyAsync())
        {
            return;
        }

        var dados = new[]
        {
            (Nome: "Maria Silva", Cpf: "52998224725", Email: "maria@exemplo.com"),
            (Nome: "João Santos", Cpf: "11144477735", Email: "joao@exemplo.com"),
            (Nome: "Ana Costa", Cpf: "93541134780", Email: "ana@exemplo.com")
        };

        foreach (var dado in dados)
        {
            var endereco = new Endereco(
                "01001000",
                "Praça da Sé",
                "100",
                null,
                "Sé",
                "São Paulo",
                "SP",
                true);

            var cliente = new Cliente(
                dado.Nome,
                dado.Cpf,
                dado.Email,
                "11999999999",
                new DateOnly(1990, 1, 1),
                endereco);

            db.Clientes.Add(cliente);
        }

        await db.SaveChangesAsync();
    }
}
