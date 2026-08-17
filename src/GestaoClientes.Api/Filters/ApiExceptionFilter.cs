using GestaoClientes.Application.Common.Exceptions;
using GestaoClientes.Application.Common.Responses;
using GestaoClientes.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace GestaoClientes.Api.Filters;
public sealed class ApiExceptionFilter(ILogger<ApiExceptionFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var (status,mensagem)=context.Exception switch
        {
            NotFoundException e=>(StatusCodes.Status404NotFound,e.Message),
            ConflictException e=>(StatusCodes.Status409Conflict,e.Message),
            DomainException e=>(StatusCodes.Status422UnprocessableEntity,e.Message),
            ArgumentException e=>(StatusCodes.Status400BadRequest,e.Message),
            _=>(StatusCodes.Status500InternalServerError,"Ocorreu um erro interno.")
        };
        if(status==500) logger.LogError(context.Exception,"Erro inesperado na API");
        context.Result=new ObjectResult(ApiResponse<object?>.Erro(mensagem)){StatusCode=status};context.ExceptionHandled=true;
    }
}
