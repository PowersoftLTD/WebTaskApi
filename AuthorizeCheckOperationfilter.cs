using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class AuthorizeCheckOperationfilter : IDocumentFilter
{
    private readonly IConfiguration configuration;

    public AuthorizeCheckOperationfilter(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    void IDocumentFilter.Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var showEndpoints = configuration.GetValue<bool>("ShowEndpoints");
        var paths = swaggerDoc.Paths.ToList();
        //var endpointsToRemove = swaggerDoc.Paths
        //    .Where(path => path.Value.Operations.Any(op => op.Value.Security.Any()))
        //    .Select(path => path.Key)
        //    .ToList();
        foreach (var path in paths)
        {
            if(!showEndpoints && path.Key.Contains("WeatherForecast"))
            {
                swaggerDoc.Paths.Remove(path.Key);
            }
            //swaggerDoc.Paths.Remove(endpoints);
        }

    }
}