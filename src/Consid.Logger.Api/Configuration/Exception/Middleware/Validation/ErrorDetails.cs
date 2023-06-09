using System.Text.Json;

namespace Consid.Logger.Api.Configuration.Exception.Middleware.Validation;

public class ErrorDetails
{

    public static string Type => "https://tools.ietf.org/html/rfc7231#section-6.5.1";
    public static string Title => "One or more validation errors occurred.";
    public int Status { get; set; }
    public string TraceId { get; set; }
    public Dictionary<string, object> Errors { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}