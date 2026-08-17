using GestaoClientes.Api.Filters;
using GestaoClientes.Application.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
namespace GestaoClientes.Api.Configurations;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiControllers(this IServiceCollection services)
    {
        services.AddControllers(options => options.Filters.Add<ApiExceptionFilter>())
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        services.Configure<ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = context =>
        {
            var mensagem = string.Join(" ", context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct());
            return new BadRequestObjectResult(ApiResponse<object?>.Erro(string.IsNullOrWhiteSpace(mensagem) ? "Dados inválidos." : mensagem));
        });
        return services;
    }
}
