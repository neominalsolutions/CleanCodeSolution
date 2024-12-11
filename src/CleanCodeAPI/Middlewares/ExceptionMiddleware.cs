using System.Net.Mime;
using System.Net;
using System.Text.Json;
using CleanCodeAPI.Models;

namespace CleanCodeAPI.Exceptions
{
  public class ExceptionMiddleware
  {
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
    {
      _logger = logger;
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await Console.Out.WriteLineAsync("Request");
        // tüm requestler try bloğu içerisine girmek zorunda kalıyor
        await _next(context); // Controllers Request

        await Console.Out.WriteLineAsync("Response");
      }
      catch (ValidationException valex) // 400 hataları
      {
        await Console.Out.WriteAsync(valex.Message);
        await HandleValidationExceptionResponseAsync(context, valex);
      }
      catch (Exception ex) // 500 hataları
      {
        _logger.LogError(ex, ex.Message);
        await HandleCustomExceptionResponseAsync(context, ex);
      }
    }


    private async Task HandleValidationExceptionResponseAsync(HttpContext context, ValidationException ex)
    {
      context.Response.ContentType = MediaTypeNames.Application.Json;
      context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

      var response = new ErrorModel(context.Response.StatusCode, ex.Message, ex.Source?.ToString());
      var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

      var json = JsonSerializer.Serialize(response, options);
      await context.Response.WriteAsync(json);
    }

    private async Task HandleCustomExceptionResponseAsync(HttpContext context, Exception ex)
    {
      context.Response.ContentType = MediaTypeNames.Application.Json;
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      var response = new ErrorModel(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString());
      var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

      var json = JsonSerializer.Serialize(response, options);
      await context.Response.WriteAsync(json);
    }
  }
}
