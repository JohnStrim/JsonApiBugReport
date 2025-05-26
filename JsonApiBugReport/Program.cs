using JsonApiBugReport.Data;
using JsonApiBugReport.Data.DummySeed;
using JsonApiBugReport.Extensions;
using JsonApiDotNetCore.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplicationDbContext(builder.Configuration)
    .AddApiConfiguration();

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
