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
            //Set Content Type (application/json)
            httpContext.Response.ContentType = "application/json";

            //Return Standard Response (ErrorDetails)
            var response = new ErrorDetails   //C# Object
            {
                StatusCode = (int)HttpStatusCode.NotFound,   //Body
                ErrorMessage = $"The Endpoint {httpContext.Request.Path} Not Found"
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //Set Content Type (application/json)
            httpContext.Response.ContentType = "application/json";
            //Set Status Code To 500
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;   //500
            //Return Standard Response (ErrorDetails)
            var response = new ErrorDetails   //C# Object
            {
                ErrorMessage = ex.Message
            };
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,   //404
                UnAuthorizedException => (int)HttpStatusCode.Unauthorized,   //401
                ValidationException validationException => HandleValidationException(validationException, response),  
                _ => (int)HttpStatusCode.InternalServerError   //500
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
