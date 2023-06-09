namespace Consid.Logger.Domain.Configuration;

public class AppConfig
{
    public string AllowedHosts { get; set; }
    public string[] AllowedOrigins { get; set; }
    
    public ExternalSources ExternalSources { get; set; }
    public AzureStorage AzureStorage { get; set; }
}

public class ExternalSources
{
    public string RedisUrl { get; set; }
}

public class AzureStorage
{
    public string ConnectionString { get; set; }
}