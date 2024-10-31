using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Pitstop.RentalManagementAPI.Exceptions;

namespace Pitstop.RentalManagementAPI.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case DbUpdateException:
            case CarAlreadyOccupiedException:
            case DateIsInThePastException:
            case EndDateIsBeforeStartDateException:
                context.Result = new BadRequestObjectResult(context.Exception.Message);
                break;
            case NotFoundException:
                context.Result = new NotFoundObjectResult(context.Exception.Message);
                break;
            default:
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = 500
                };
                break;
        }

        context.ExceptionHandled = true;
    }
}