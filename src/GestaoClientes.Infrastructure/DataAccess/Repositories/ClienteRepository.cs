using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Enums;
using GestaoClientes.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
namespace GestaoClientes.Infrastructure.DataAccess.Repositories;
public sealed class ClienteRepository(GestaoClientesDbContext db) : IClienteRepository
{
    public Task<bool> CpfExisteAsync(string cpf,int? id=null)=>db.Clientes.AnyAsync(x=>x.Cpf==cpf&&(!id.HasValue||x.Id!=id));
    public Task<bool> EmailExisteAsync(string email,int? id=null)=>db.Clientes.AnyAsync(x=>x.Email==email&&(!id.HasValue||x.Id!=id));
    public Task<Cliente?> ObterAsync(int id)=>db.Clientes.Include(x=>x.Enderecos).SingleOrDefaultAsync(x=>x.Id==id);
    public async Task<(IReadOnlyList<Cliente>,int)> ListarAsync(int pagina,int tamanho,string? nome,string? cpf,StatusCliente? status)
    {
        var q=db.Clientes.Include(x=>x.Enderecos).AsNoTracking().AsQueryable();
        if(!string.IsNullOrWhiteSpace(nome))q=q.Where(x=>x.Nome.Contains(nome));
        if(!string.IsNullOrWhiteSpace(cpf))q=q.Where(x=>x.Cpf.Contains(cpf));
        q=q.Where(x=>x.Status==(status??StatusCliente.Ativo));
        var total=await q.CountAsync();
        return(await q.OrderBy(x=>x.Nome).Skip((pagina-1)*tamanho).Take(tamanho).ToListAsync(),total);
    }
    public Task AdicionarAsync(Cliente cliente)=>db.Clientes.AddAsync(cliente).AsTask();
}
