using System.Diagnostics;
using Consid.Logger.Api.Configuration.Exception.Middleware.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Consid.Logger.Api.Configuration.Validation;

public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        
        const int statusCode = 400;
            
        var errors = context.ModelState
            .Where(ms => ms.Value.Errors.Any())
            .Select(x => new
            {
                PropertyName = x.Key, 
                ErrorMessage = x.Value.Errors
            }).ToList();
            
        var errorsList = new Dictionary<string, object>();
        foreach (var error in errors.DistinctBy(x=>x.PropertyName))
        {
            errorsList.Add(
                error.PropertyName, 
                errors
                    .Where(x=> x.PropertyName == error.PropertyName)
                    .Select(x=>
                        new {
                            Message = x.ErrorMessage[0]?.ErrorMessage,
                            Code = "RequireValidator"
                        }));
        }

        var result = new ErrorDetails()
        {
            Status = statusCode,
            TraceId = Activity.Current?.Id ?? context.HttpContext?.TraceIdentifier,
            Errors = errorsList
        };

        context.Result = new JsonResult(result)
        {
            StatusCode = statusCode
        };
    }
}