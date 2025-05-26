using System;
using System.Collections.Generic;
using JsonApiBugReport;
using JsonApiBugReport.Extensions;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Queries.QueryableBuilding;
using JsonApiDotNetCore.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplicationDbContext(builder.Configuration)
    .AddApiConfiguration();

if (typeof(IIdentifiable).Assembly.GetName().Version >= new Version(5, 7,2))
{
    builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
    {
        ["Logging:LogLevel:JsonApiDotNetCore.Repositories"] = "Debug"
    });
}
else
{
    builder.Services.AddTransient<IQueryableBuilder, LoggingQueryableBuilder>();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseJsonApi();

app.MapControllers();

await app.RunDbMigrations();

await app.RunAsync();
