using Castle.DynamicProxy;
using System.Diagnostics;

namespace CleanCodeAPI.Aspects
{

  // Bu attribute sadece Methodlar için kullanılabilir.
  // Aspect Oriented Programlama Runtime da Reflection ile birlikte çalışan bir programalama modeli.
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
  public class BenchmarkAspect :Attribute, IInterceptor
  {
    private Stopwatch stopWatch;

    public BenchmarkAspect()
    {
      stopWatch = new Stopwatch();
    }

    // ilgili servisin methodu üzerine yazdığımızda cross cutting bir işlem için araya girip tetiklenecek olan method
    public void Intercept(IInvocation invocation)
    {
     
      var method = invocation.MethodInvocationTarget ?? invocation.Method;
      // method üzerinde bir aspect tanımlımı kontrolü BenchmarkAspect
      var attributeExists = method.GetCustomAttributes(typeof(BenchmarkAspect), true).Any();

      if (attributeExists)
      {
        // OnBefore
        stopWatch.Start();
        Console.WriteLine($"Start Time " + stopWatch.ElapsedMilliseconds);

        // OnProcessing Kısmı
        invocation.Proceed(); // await next ile araya girdiğimiz kod bloğundaki methoda dön

        stopWatch.Stop();
        Console.WriteLine($"Stop Time " + stopWatch.ElapsedMilliseconds);
        // OnAfter
      }
      else
      {
        // atribute yoksa logic uygulamadan bir sonraki adıma geç.
        invocation.Proceed();
      }

    }
  }
}
