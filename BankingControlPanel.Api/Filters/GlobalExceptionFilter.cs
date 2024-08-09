using BankingControlPanel.Application.Exceptions;
using BankingControlPanel.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BankingControlPanel.Api.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {

            _logger.LogError("Error Message: {exceptionMessage}, Time of occurrence {time}", context.Exception.Message, DateTime.UtcNow);

            (string? Detail, string Title, int StatusCode) details = context.Exception switch
            {
                AppException =>
                (
                    context.Exception.Message,
                    context.Exception.GetType().Name,
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                DomainException =>
                (
                    context.Exception.Message,
                    context.Exception.GetType().Name,
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                ValidationException =>
                (
                    context.Exception.Message,
                    context.Exception.GetType().Name,
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                _ =>
                (
                    context.Exception.Message,
                    context.Exception.GetType().Name,
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = context.HttpContext.Request.Path
            };

            problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

            if (context.Exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
            }

            context.HttpContext.Response.WriteAsJsonAsync(problemDetails).Wait();

            context.ExceptionHandled = true;
        }
    }
}
