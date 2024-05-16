using FinanceManager.Communication.Responses.Register;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinanceManager.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is FinanceManagerException)
        {
            HandleProjectException(context);
        }
        else
        {
             ThrowUnknownError(context);
        }
        
    }

    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException errorOnValidationException)
        {
            ResponseErrorJson errorResponse = new(errorOnValidationException.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
        else if (context.Exception is NotFoundException notFoundException)
        {
            ResponseErrorJson errorResponse = new(notFoundException.Message);
            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
        else
        {
            ResponseErrorJson errorResponse = new(context.Exception.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessage.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
