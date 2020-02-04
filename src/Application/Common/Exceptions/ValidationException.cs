using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace CleanArchitecture.Application.Common.Exceptions
{
    public class ValidationDetail 
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Attribute { get; set; }
    }

    public class ValidationException : BusinessException
    {
        public IList<ValidationDetail> Failures { get; }

        public ValidationException()
            : base("Ca.0001", "One or more validation failures have occurred.")
        {
            Failures = new List<ValidationDetail>();
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {            
            foreach (var failure in failures)
            {
                Failures.Add(new ValidationDetail()
                {
                    Code = failure.ErrorCode,
                    Message = failure.ErrorMessage,
                    Attribute = failure.PropertyName
                });
            }
        }
    }
}
