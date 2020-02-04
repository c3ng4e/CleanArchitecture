using System;

namespace CleanArchitecture.Application.Common.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string name, object key)
            : base("Ca.0002", $"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
