using System;
using System.Net;

namespace CleanArchitecture.WebUI.Services
{
    public interface IHttpExceptionStatusCodeFinder
    {
        HttpStatusCode GetStatusCode(HttpListenerContext httpContext, Exception exception);
    }
}
