using GestaoClientes.Domain.Repositories;
using GestaoClientes.Infrastructure.DataAccess;
using GestaoClientes.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoClientes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("SqlServer")
            ?? throw new InvalidOperationException("A conexão 'SqlServer' não foi configurada.");

        services.AddDbContext<GestaoClientesDbContext>(options => options.UseSqlServer(connection));
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
