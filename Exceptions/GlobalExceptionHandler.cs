
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace StudentManagement.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An unhandled exception occured.");
            var problemDetails = new ProblemDetails
            {
                Title = "An error occuerd",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };
            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
