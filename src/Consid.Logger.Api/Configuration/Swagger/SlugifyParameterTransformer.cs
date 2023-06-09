using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace Consid.Logger.Api.Configuration.Swagger;

public partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        return value == null
            ? null
            : SlugifyRegex().Replace(value.ToString() ?? string.Empty, "$1-$2").ToLower();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex SlugifyRegex();
}