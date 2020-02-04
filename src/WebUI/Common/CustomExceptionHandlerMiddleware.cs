using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Common
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            if (exception is ValidationException validationException) 
            {
                var result = GetResult(context, validationException);
                return ExecuteObjectResultAsync(context, result);
            }

            return DefaultExceptionHandling(context, exception);
        }

        //public IActionResult GetResult<TException>(HttpContext context, TException exception) where TException : IBusinessException 
        //{
        //    var converter = context.RequestServices.GetService<IExceptionConverter<TException>>();
        //    if (converter == null) 
        //    {
        //        Console.WriteLine("derp");
        //    }
        //    return converter.Convert(exception);
        //}

        public IActionResult GetResult(HttpContext context, ValidationException exception)
        {
            var converter = context.RequestServices.GetService<IExceptionConverter<ValidationException>>();
            return converter.Convert(exception);
        }

        public Task ExecuteObjectResultAsync<TResult>(HttpContext context, TResult result) where TResult : IActionResult
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (result == null) throw new ArgumentNullException(nameof(result));

            var routeData = context.GetRouteData() ?? new RouteData();
            var actionContext = new ActionContext(context, routeData, new ActionDescriptor());

            return result.ExecuteResultAsync(actionContext);
        }

        public Task DefaultExceptionHandling(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            switch (exception)
            {                
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.Failures);
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
