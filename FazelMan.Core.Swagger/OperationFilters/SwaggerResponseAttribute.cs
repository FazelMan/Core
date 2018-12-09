using System;
using System.Net;

namespace Core.Swagger.OperationFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerResponseAttribute : Attribute
    {
        public SwaggerResponseAttribute(HttpStatusCode statusCode, Type type,string description = null)
        {
            this.StatusCode = (int)statusCode;
            this.Description = description;
            this.ResponseType = type;
        }

        public int StatusCode { get; private set; }

        public string Description { get; private set; }

        public Type ResponseType { get; private set; }
    }
}