namespace Consid.Logger.AzureFunction.Functions.Http.Dto;

public class BadRequestDto
{
    public object Errors { get; private set; }
    public int StatusCode { get; private set; }

    public BadRequestDto(object errors, int statusCode)
    {
        Errors = errors;
        StatusCode = statusCode;
    }
}