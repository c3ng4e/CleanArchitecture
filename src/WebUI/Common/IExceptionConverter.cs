using CleanArchitecture.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Common
{
    public interface IExceptionConverter<TException> where TException : IBusinessException
    {
        IActionResult Convert(TException exception);
    }
}
