using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace CleanArchitecture.WebUI.Services
{
    public interface IHttpExceptionStatusCodeFinder
    {
        HttpStatusCode GetStatusCode(Exception exception);
    }
}
