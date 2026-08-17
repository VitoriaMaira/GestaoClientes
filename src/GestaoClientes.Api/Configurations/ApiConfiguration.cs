using GestaoClientes.Api.Filters;
using GestaoClientes.Application.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace GestaoClientes.Api.Configurations;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiControllers(this IServiceCollection services)
    {
        services
            .AddControllers(options => options.Filters.Add<ApiExceptionFilter>())
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.Configure<ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = context =>
        {
            var erros = context.ModelState.Values
                .SelectMany(valor => valor.Errors)
                .Select(erro => erro.ErrorMessage)
                .Where(mensagem => !string.IsNullOrWhiteSpace(mensagem))
                .Distinct();

            var mensagem = string.Join(" ", erros);
            var resposta = string.IsNullOrWhiteSpace(mensagem)
                ? "Dados inválidos."
                : mensagem;

            return new BadRequestObjectResult(ApiResponse<object?>.Erro(resposta));
        });

        return services;
    }
}
