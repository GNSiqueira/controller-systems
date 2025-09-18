using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ControlSystems.Objects.Contracts.Exceptions.Base;

namespace ControlSystems.Objects.Contracts.Exceptions;

public class HttpExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Response<object> response;

        if (context.Exception is ApiException exception)
        {
            response = new Response<object>(status: exception.StatusCode, message: exception.Message);

            if (exception is _ValidationException validation && validation.Errors != null && validation.Errors.Count() > 0)
            {
                response = new Response<object>(status: exception.StatusCode, message: exception.Message, errors: validation.Errors);
            }
        }
        else
        {
            response = new Response<object>(500, "Ocorreu um erro inesperado no servidor.");

            // Ex: _logger.LogError(context.Exception, "An unhandled exception has occurred.");
        }

        context.Result = new ObjectResult(response)
        {
            StatusCode = response.Status
        };

        context.ExceptionHandled = true;

    }
}