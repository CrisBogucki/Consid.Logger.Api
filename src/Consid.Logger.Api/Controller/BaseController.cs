using System.Net.Mime;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Consid.Logger.Api.Controller;

/// <summary>
/// Base Controller
/// </summary>
[EnableCors("CorsPolicy")]
[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class BaseController : ControllerBase
{
    
}