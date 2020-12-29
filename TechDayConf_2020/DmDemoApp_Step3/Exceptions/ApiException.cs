using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmDemoApp.Exceptions
{
    public class ApiException : Exception
    {

        public ApiExceptionType Type { get; private set; }


        public ApiException(string message, ApiExceptionType type)
            : base(message)
        {
            Type = type;
            Debug.WriteLine("ApiException: " + message);
        }

        public ApiException(string message, Exception innerException, ApiExceptionType type) : base(message, innerException)
        {
            Debug.WriteLine("ApiException: " + message + " - " + innerException.Message);
            Type = type;
        }
    }

    public enum ApiExceptionType
    {
        NotInitialized,
        UnexpectedError,
        ServerError,
        InvalidServerResponse,
        NoServerResponse,
        InvalidUserAgent,
        CacheError
    }
}
