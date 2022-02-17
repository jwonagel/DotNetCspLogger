using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var conf = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var path = conf.GetValue<string>("LogPath");

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .WriteTo.Console()
    .WriteTo.File(path,
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true));

builder.Services.AddControllers(conf =>
{
    conf.InputFormatters.OfType<SystemTextJsonInputFormatter>().First().SupportedMediaTypes.Add(
        MediaTypeHeaderValue.Parse("application/csp-report")
    );
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "csplogger", Version = "v1" });
});

var app = builder.Build();


app.UseDeveloperExceptionPage();
app.UseSwagger();
var virtualPath = conf.GetValue<string>("VirtualPath") ?? string.Empty;
virtualPath = EnsureTrailingSlash(virtualPath);

app.UseSwaggerUI(c => c.SwaggerEndpoint($"{virtualPath}swagger/v1/swagger.json", "csplogger v1"));

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();

string EnsureTrailingSlash(string input)
{
    if (!input.EndsWith("/"))
    {
        return input + "/";
    }

    return input;
}