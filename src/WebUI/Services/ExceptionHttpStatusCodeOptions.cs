using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Services
{
    public class ExceptionHttpStatusCodeOptions
    {
        public IDictionary<string, HttpStatusCode> ErrorCodeToHttpStatusCodeMappings { get; }

        public ExceptionHttpStatusCodeOptions() 
        {
            ErrorCodeToHttpStatusCodeMappings = new Dictionary<string, HttpStatusCode>();
        }

        public void Map(string errorCode, HttpStatusCode httpStatusCode) 
        {
            ErrorCodeToHttpStatusCodeMappings[errorCode] = httpStatusCode;
        }
    }
}
