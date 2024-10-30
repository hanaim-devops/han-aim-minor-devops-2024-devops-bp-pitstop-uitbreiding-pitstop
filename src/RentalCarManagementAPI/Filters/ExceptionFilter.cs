using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Pitstop.RentalCarManagementAPI.Exceptions;

namespace Pitstop.RentalManagementAPI.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is LicensePlateAlreadyRegistered)
        {
            context.Result = new BadRequestResult();
        }
        context.ExceptionHandled = true;
    }
}