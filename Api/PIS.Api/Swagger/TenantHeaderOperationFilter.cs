using Microsoft.OpenApi.Models;
using PIS.Api.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PIS.Api.Swagger
{
    internal class TenantHeaderOperationFilter : IOperationFilter
    {
        /// <summary>
        /// An operation filter adding tenant parameter to Swagger UI.
        /// </summary>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters is null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = HeaderConstants.Tenant,
                In = ParameterLocation.Header,
                Required = true
            });
        }
    }
}
