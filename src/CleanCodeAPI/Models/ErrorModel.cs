namespace CleanCodeAPI.Models
{
  public class ErrorModel
  {
    public string? Message { get; set; }
    public string? Details { get; set; }

    public ErrorModel(int statusCode, string? message, string? details = null)
    {
      Message = message;
      Details = details;
    }
  }
}
