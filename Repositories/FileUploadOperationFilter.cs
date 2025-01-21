using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskManagement.API.Repositories
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileParameters = context.ApiDescription.ParameterDescriptions
                .Where(p => p.ParameterDescriptor.ParameterType == typeof(IFormFile) || p.ParameterDescriptor.ParameterType == typeof(List<IFormFile>));

            foreach (var fileParam in fileParameters)
            {
                var fileParamDescription = operation.Parameters.FirstOrDefault(p => p.Name == fileParam.Name);
                if (fileParamDescription != null)
                {
                    fileParamDescription.Schema.Type = "file";  // Mark it as a file input
                }
            }
        }
    }
}
