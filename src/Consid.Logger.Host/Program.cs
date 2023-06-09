using Consid.Logger.Adapter.Crone.Configuration;
using Consid.Logger.Api.Configuration;
using Consid.Logger.Api.Configuration.Exception;
using Consid.Logger.Application.Configuration;
using Consid.Logger.Host;

var builder = WebApplication.CreateBuilder(args);
var appConfig = builder.AddAppConfiguration();

//builder.AddSerilogConfiguration();
builder.AddApiConfiguration(appConfig);

builder.Services.AddApplicationConfiguration();
builder.Services.AddAdapterCroneConfiguration();
builder.WebHost.UseKestrel(options => options.AddServerHeader = false);

var app = builder.Build();

app.AddApiConfiguration();
app.AddGlobalExceptionConfiguration();

app.Run();

