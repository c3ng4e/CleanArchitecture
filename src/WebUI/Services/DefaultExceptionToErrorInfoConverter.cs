using Microsoft.Extensions.Options;
using System;
using System.Net;

namespace CleanArchitecture.WebUI.Services
{
    public class DefaultExceptionToErrorInfoConverter : IHttpExceptionStatusCodeFinder
    {
        protected ExceptionHttpStatusCodeOptions Options { get; }

        public DefaultExceptionToErrorInfoConverter(IOptions<ExceptionHttpStatusCodeOptions> options) 
        {
            Options = options.Value;
        }

        public HttpStatusCode GetStatusCode(HttpListenerContext httpContext, Exception exception)
        {
            if (Options.ErrorCodeToHttpStatusCodeMappings.TryGetValue("Ca.0001", out var status)) 
            {
                return status;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
