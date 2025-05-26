using JsonApiBugReport.Data;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Resources.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JsonApiBugReport.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services
            .AddLogging()
            .AddOptions()
            .AddHttpContextAccessor()
            .Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddControllers();

        services
            .AddJsonApiSpecification();

        return services;
    }

    private static IServiceCollection AddJsonApiSpecification(this IServiceCollection services)
    {
        services.AddJsonApi<ApplicationDbContext>(
            options: o => o.AddJsonApiOptions(),
            discovery: d => d.AddCurrentAssembly());

        return services;
    }

    public static JsonApiOptions AddJsonApiOptions(this JsonApiOptions options)
    {
        options.AllowUnknownQueryStringParameters = true;
        options.DefaultPageSize = new PageSize(25);
        options.MaximumPageNumber = new PageNumber(200);
        options.MaximumPageSize = new PageSize(100);
        options.RelationshipLinks = LinkTypes.None;
        options.ResourceLinks = LinkTypes.None;
        options.TopLevelLinks = LinkTypes.None;
        options.MaximumIncludeDepth = 10;
        options.SerializerOptions.DictionaryKeyPolicy = null;
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.IncludeTotalResourceCount = true;

#if DEBUG
        // These are the debug options never to be opened in production mode
        options.IncludeExceptionStackTraceInErrors = true;
        options.IncludeRequestBodyInErrors = true;
        options.SerializerOptions.WriteIndented = true;
#endif

        return options;
    }
}
