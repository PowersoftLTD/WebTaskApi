using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskManagement.API.Repositories
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class OpenApiParameterIgnoreAttribute : System.Attribute
    {
    }
    public class OpenApiParameterIgnoreFilter //: Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        //public void Apply(OpenApiOperation operation, OperationFilterContext context)
        //{
        //    if (operation == null || context == null || context.ApiDescription == null || context.ApiDescription.ParameterDescriptions == null)
        //        return;

        //    // Identify parameters to hide
        //    var parametersToHide = context.ApiDescription.ParameterDescriptions
        //        .Where(param =>
        //            param.ModelMetadata?.Attributes
        //                .OfType<OpenApiParameterIgnoreAttribute>()
        //                .Any() == true)
        //        .Select(param => param.Name)
        //        .ToList();

        //    // Remove the parameters from the operation
        //    foreach (var parameterName in parametersToHide)
        //    {
        //        var parameter = operation.Parameters
        //            .FirstOrDefault(param => string.Equals(param.Name, parameterName, System.StringComparison.Ordinal));

        //        if (parameter != null)
        //        {
        //            operation.Parameters.Remove(parameter);
        //        }
        //    }
        //}

        //private static bool ParameterHasIgnoreAttribute(Microsoft.AspNetCore.Mvc.ApiExplorer.ApiParameterDescription parameterDescription)
        //{
        //    if (parameterDescription.ModelMetadata == null)
        //        return false;
        //    if (parameterDescription.ModelMetadata is Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.DefaultModelMetadata metadata)
        //    {
        //        // return metadata.Attributes.ParameterAttributes.Any(attribute => attribute.GetType() == typeof(OpenApiParameterIgnoreAttribute));
        //        var parameterAttributes = metadata.Attributes.ParameterAttributes;
        //        if (parameterAttributes != null)
        //        {
        //            return parameterAttributes.Any(attribute => attribute.GetType() == typeof(OpenApiParameterIgnoreAttribute));
        //        }
        //    }

        //    return false;
        //}
    }
}
