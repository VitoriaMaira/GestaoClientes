using GestaoClientes.Domain.Repositories;
namespace GestaoClientes.Infrastructure.DataAccess.Repositories;
public sealed class UnitOfWork(GestaoClientesDbContext db) : IUnitOfWork { public Task SalvarAsync() => db.SaveChangesAsync(); }
