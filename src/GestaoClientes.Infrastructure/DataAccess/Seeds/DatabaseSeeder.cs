using GestaoClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace GestaoClientes.Infrastructure.DataAccess.Seeds;
public static class DatabaseSeeder
{
    public static async Task SemearAsync(GestaoClientesDbContext db)
    {
        await db.Database.MigrateAsync(); if(await db.Clientes.AnyAsync()) return;
        var dados=new[]{("Maria Silva","52998224725","maria@exemplo.com"),("João Santos","11144477735","joao@exemplo.com"),("Ana Costa","93541134780","ana@exemplo.com")};
        foreach(var d in dados) db.Clientes.Add(new Cliente(d.Item1,d.Item2,d.Item3,"11999999999",new DateOnly(1990,1,1),new Endereco("01001000","Praça da Sé","100",null,"Sé","São Paulo","SP",true)));
        await db.SaveChangesAsync();
    }
}
