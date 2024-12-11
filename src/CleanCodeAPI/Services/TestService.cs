using CleanCodeAPI.Aspects;
using CleanCodeAPI.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CleanCodeAPI.Services
{

  // Net Core Aspect Oriented olarak Controller Action bazlı ActionFilterAttribute sınıfı ile olur veya Request Bazlı Middleware gibi yapılar ile uygulama bazı bir AOP destekler. Ama kendi servislerimiz için özel olarak bunu geliştirmemiz gerekir.

  // Service Layer
  public class TestService: ITestService
  {
    // Controller Action olmadığı için tetiklenmedi.Servis katmanında bu attribute çalışmaz. 
    // [ServiceFilter(typeof(RequestInterceptionAttribute))]
    [BenchmarkAspect]
    public void Handle()
    {
      //Stopwatch sp = new Stopwatch();
      //sp.Start();
      //Console.Out.WriteLine("On Before" + sp.ElapsedMilliseconds);

      Thread.SpinWait(1000000);

      Console.Out.WriteLine("Test Service Logic");

      //sp.Stop();
      //Console.Out.WriteLine("On After" + sp.ElapsedMilliseconds);

    }
  }
}
