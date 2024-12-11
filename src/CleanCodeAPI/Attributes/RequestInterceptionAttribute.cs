using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanCodeAPI.Attributes
{
  // Bu tarz tanımlamalar framework tarafından aspect orinted programlamayı destekler.
  // Sadece Action üzerine koyarak bu işlemleri yönetebiliriz. Framework ait bir özellik.
  public class RequestInterceptionAttribute : ActionFilterAttribute
  {
    private readonly ILogger<RequestInterceptionAttribute> logger;

    public RequestInterceptionAttribute(ILogger<RequestInterceptionAttribute> logger)
    {
      this.logger = logger;
    }

    public RequestInterceptionAttribute()
    {

    }

    /// <summary>
    /// Action'a girmeden önce yapılacak olan cross cutting işlemleri yazdığımız method OnBefore
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      this.logger.LogInformation("OnBefore");
      base.OnActionExecuting(context);
    }

    /// <summary>
    /// Actiondan çıktıktan sonra yapılacak olan cross cutting işlemler.
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
      this.logger.LogInformation("OnAfter");
      base.OnActionExecuted(context);

    }

  }
}
