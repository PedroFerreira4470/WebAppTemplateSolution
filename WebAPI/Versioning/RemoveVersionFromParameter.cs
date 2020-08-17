using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace WebAPI.Versioning
{
    public class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.Any())
            {
                var versionParam = operation.Parameters.Single(p => p.Name == "version");
                operation.Parameters.Remove(versionParam);
            }
        }
    }
}
