using Consid.Logger.Adapter.Crone.Configuration;
using Consid.Logger.Adapter.ExternalSource.Configuration;
using Consid.Logger.Api.Configuration;
using Consid.Logger.Api.Configuration.Exception;
using Consid.Logger.Application.Configuration;
using Consid.Logger.Host;
using Consid.Logger.Persist.AzureStorageTable.Configuration;

var builder = WebApplication.CreateBuilder(args);
var appConfig = builder.AddAppConfiguration();

// Presentation layer configuration
builder.AddApiConfiguration(appConfig);

// Application layer configuration
builder.Services.AddApplicationConfiguration();

// Adapter layer configuration
builder.Services.AddAdapterCroneConfiguration();
builder.Services.AddAdapterExternalSourceConfiguration();

// Persist layer configuration
builder.Services.AddRepositoryAzureStorageTableConfiguration();

builder.WebHost.UseKestrel(options => options.AddServerHeader = false);

var app = builder.Build();

app.AddApiConfiguration();
app.AddGlobalExceptionConfiguration();

app.Run();

