using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.WebUI.Common
{
    public class DefaultValidationExceptionConverter : IExceptionConverter<ValidationException>
    {
        private readonly IHttpExceptionStatusCodeFinder _statusCodeFinder;

        public DefaultValidationExceptionConverter(IHttpExceptionStatusCodeFinder statusCodeFinder) 
        {
            _statusCodeFinder = statusCodeFinder;
        }

        public IActionResult Convert(ValidationException exception)
        {
            var errors = new Dictionary<string, string[]>();
            var failureGroups = exception.Failures
                    .GroupBy(e => e.Attribute, e => e.Message);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                errors.Add(propertyName, propertyFailures);
            }

            var validation = new ValidationProblemDetails(errors)
            {
                Detail = "Validation failed",
                Title = exception.Message,
                Status = (int)_statusCodeFinder.GetStatusCode(exception),
                Instance = "TODO",
                Type = "TODO"
            };

            return new BadRequestObjectResult(validation);
        }
    }
}
