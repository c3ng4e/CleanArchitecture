using CleanArchitecture.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
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

        public HttpStatusCode GetStatusCode(Exception exception)
        {
            if (exception is IHasErrorCode exceptionWithErrorCode && !string.IsNullOrWhiteSpace(exceptionWithErrorCode.Code))
            {
                if (Options.ErrorCodeToHttpStatusCodeMappings.TryGetValue(exceptionWithErrorCode.Code, out var status)) 
                {
                    return status;
                }
            }

            // TODO: Auth exception?

            if (exception is ValidationException) 
            {
                return HttpStatusCode.BadRequest;
            }

            if (exception is NotImplementedException) 
            {
                return HttpStatusCode.NotImplemented;
            }

            if (exception is IBusinessException) 
            {
                return HttpStatusCode.Forbidden;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
