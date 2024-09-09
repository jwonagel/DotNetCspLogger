using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Settings.Configuration;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration, new ConfigurationReaderOptions
    {
        SectionName = "Serilog"
    })
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
builder.Host.UseSerilog();

builder.Services.AddControllers(opt =>
{
    opt.InputFormatters.OfType<SystemTextJsonInputFormatter>().First().SupportedMediaTypes.Add(
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
var virtualPath = builder.Configuration.GetValue<string>("VirtualPath") ?? string.Empty;
virtualPath = EnsureTrailingSlash(virtualPath);

app.UseSwaggerUI(c => c.SwaggerEndpoint($"{virtualPath}swagger/v1/swagger.json", "csplogger v1"));

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

string EnsureTrailingSlash(string input)
{
    if (!input.EndsWith("/"))
    {
        return input + "/";
    }

    return input;
}