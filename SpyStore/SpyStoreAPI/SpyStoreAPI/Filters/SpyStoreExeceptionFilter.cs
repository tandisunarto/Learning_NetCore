using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SpyStoreDAL.Exceptions;

namespace SpyStoreAPI.Filters
{
    public class SpyStoreExeceptionFilter : IExceptionFilter
    {
        private readonly bool _isDevelopment;

        public SpyStoreExeceptionFilter(bool isDevelopment)
        {
            _isDevelopment = isDevelopment;
        }
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            string stackTrace = _isDevelopment ? context.Exception.StackTrace : String.Empty;
            string message = ex.Message;
            string error = String.Empty;
            IActionResult actionResult;
            if (ex is InvalidQuantityException)
            {
                //Returns a  400
                error = "Invalid quantity request.";
                actionResult = new BadRequestObjectResult(CreateContent(error,message,stackTrace));
            }
            else if (ex is DbUpdateConcurrencyException)
            {
                //Returns a  400
                error = "Concurrency Issue.";
                actionResult = new BadRequestObjectResult(CreateContent(error, message, stackTrace));
            }
            else
            {
                error = "General error.";
                actionResult = new ObjectResult(CreateContent(error, message, stackTrace))
                {
                    StatusCode = 500
                };
            }
            context.Result = actionResult;
        }

        private static object CreateContent(string error, string message, string stackTrace)
        {
            return new
            {
                Error = error,
                Message = message,
                StackTrace = stackTrace
            };
        }
    }
}
