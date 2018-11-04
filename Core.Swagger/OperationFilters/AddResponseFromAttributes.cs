using System;
using System.Linq;
using System.Net;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Swagger.OperationFilters
{
    public class AddResponseFromAttributes : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var attributes = context.ApiDescription.ActionAttributes().OfType<SwaggerResponseAttribute>().ToList();

            if (!attributes.Any())
            {
                return;
            }

            foreach (var attr in attributes)
            {
                var httpCode = attr.StatusCode.ToString();
                var description = attr.Description;
                if (description == null)
                {
                    // if we don't have a description, try to get it out of HttpStatusCode
                    HttpStatusCode val;
                    if (Enum.TryParse(attr.StatusCode.ToString(), true, out val))
                    {
                        description = val.ToString();
                    }
                }

                var response = new Response { Description = description };
                if (attr.ResponseType != null)
                {
                    response = new Response
                    {
                        Description = description,
                        Schema = context.SchemaRegistry.GetOrRegister(attr.ResponseType)
                    };
                }

                operation.Responses.Remove(httpCode);
                operation.Responses.Add(httpCode, response);
            }
        }
    }
}