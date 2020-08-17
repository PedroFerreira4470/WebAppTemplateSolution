using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;

namespace Application.Common.CustomExceptions
{
    public class RestException : Exception, IRestExceptionModel
    {
        public HttpStatusCode Code { get; }
        public Dictionary<string, string[]> Errors { get; }

        public RestException()
            : base("Something wrong happen!") { }

        public RestException(HttpStatusCode code, Dictionary<string, string[]> errors = null)
        {
            Code = code;
            Errors = errors;
        }

        public RestException(HttpStatusCode code, string propertyName, string errorMessage)
        {
            Code = code;
            Errors = new Dictionary<string, string[]>
            {
                { propertyName, new [] {errorMessage} }
            };
        }

        public RestException(HttpStatusCode code, string propertyName, params string[] errorMessage)
        {
            Code = code;
            Errors = new Dictionary<string, string[]>
            {
                { propertyName, errorMessage }
            };
        }
    }
}
