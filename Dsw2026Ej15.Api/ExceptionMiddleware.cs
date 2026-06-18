using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Dsw2026Ej15.Api
{
    public class ValidationException : Exception
    {public ValidationException(string message) : base(message) { }
    }
    public class ExceptionMiddleware
    {        private readonly RequestDelegate _next;
                public ExceptionMiddleware(RequestDelegate next)
        {       _next = next;        }
                public async Task InvokeAsync(HttpContext context)
        {   try
            {    await _next(context);            }
            catch (ValidationException ex)
            {   context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (Exception)
            {   context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = "Problem" });
            }
        }
    }
}