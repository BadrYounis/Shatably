using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;
        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        //Response (StatusCode, ErrorMsg)
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                    await HandleNotFoundApiAsync(httpContext);
            }
            catch (Exception ex)
            {
                //Log Exception
                _logger.LogError($"Something Went Wrong: {ex.Message}");
                //Handle Exception
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleNotFoundApiAsync(HttpContext httpContext)
        {
            if (httpContext.Response.HasStarted)
            {
                _logger.LogWarning("Cannot handle 404, response has already started.");
                return;
            }

            httpContext.Response.ContentType = "application/json";

            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorMessage = $"The Endpoint {httpContext.Request.Path} Not Found"
            }.ToString();

            await httpContext.Response.WriteAsync(response);
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            if (httpContext.Response.HasStarted)
            {
                _logger.LogWarning("Cannot handle exception, response has already started.");
                return;
            }

            httpContext.Response.ContentType = "application/json";

            var response = new ErrorDetails
            {
                ErrorMessage = ex.Message
            };

            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                UnAuthorizedException => (int)HttpStatusCode.Unauthorized,
                ValidationException validationException => HandleValidationException(validationException, response),
                _ => (int)HttpStatusCode.InternalServerError
            };

            response.StatusCode = httpContext.Response.StatusCode;

            await httpContext.Response.WriteAsync(response.ToString());
        }
        private int HandleValidationException(ValidationException validationException, ErrorDetails response)
        {
            response.errors = validationException.Errors;
            return (int)HttpStatusCode.BadRequest;  //400
        }
    }
}
