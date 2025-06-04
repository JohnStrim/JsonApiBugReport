using JsonApiBugReport.Data;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text.Json.Serialization;

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
            .AddJsonApiSpecification()
            .AddResourceDefinitions();

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

    private static IServiceCollection AddResourceDefinitions(this IServiceCollection services)
    {
        var dataAssembly = typeof(ApplicationDbContext).Assembly;
        var identifiableType = typeof(IIdentifiable<>);
        var commonDefinitionType = typeof(JsonApiBugReport.ResourceDefinitions.CommonDefinition<,>);
        var resourceDefinitionType = typeof(IResourceDefinition<,>);

        var resourceTypes = dataAssembly.GetTypes()
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                t.Namespace == "JsonApiBugReport.Data" &&
                t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == identifiableType))
            .ToList();

        foreach (var resourceType in resourceTypes)
        {
            var identifiableInterface = resourceType.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == identifiableType);

            var idType = identifiableInterface.GetGenericArguments()[0];

            var definitionType = commonDefinitionType.MakeGenericType(resourceType, idType);
            var serviceType = resourceDefinitionType.MakeGenericType(resourceType, idType);

            services.AddScoped(serviceType, definitionType);
        }

        return services;
    }
}
