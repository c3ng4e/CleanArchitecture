using System;

namespace CleanArchitecture.Application.Common.Exceptions
{
    public class BusinessException : Exception, IBusinessException, IHasErrorCode, IHasErrorDetails
    {
        public string Code { get; protected set;  }

        public string Details { get; protected set; }

        public BusinessException(string code, string message = null, string details = null) : base(message)
        {
            Code = code;
            Details = details;
        }
    }
}
