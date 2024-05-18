using FinanceManager.Communication.Responses;
using FinanceManager.Communication.Responses.Expenses.Register;
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

        var financeManagerException = (FinanceManagerException)context.Exception;
        ResponseErrorJson errorResponse = new(financeManagerException.GetErrors());
        
        context.HttpContext.Response.StatusCode = financeManagerException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(context.Exception.Message);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
