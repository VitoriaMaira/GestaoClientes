using Microsoft.OpenApi.Models;
namespace GestaoClientes.Api.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services) { services.AddEndpointsApiExplorer(); services.AddSwaggerGen(o => o.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestão de Clientes API", Version = "v1", Description = "Cadastro, consulta e gerenciamento de endereços de clientes." })); return services; }
}
