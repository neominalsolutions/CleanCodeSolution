using CleanCodeSolution.Application.Requests;
using CleanCodeSolution.Domain.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeSolution.Application.Handlers
{
  // Facade görevi üstlenir. Facade Desing Pattern üst seviye işlemler ile alt seviye işlemlerin karmaşıklığını birbirinden ayıran bir tasarım deseni
  // Application Layer yer alan bu servis, Domain Layerdeki Bussiness kuralların uygulanaması için bir koordinatör göre görüyor. Ve bu sayede Prensentation katmaanından gelen istekleri Domain Layer aktaran arabir katman görevi üstleniyor.

  // Account ile ilgili her bir use case için (CreateAccount, BlockAccount, CloseAccount) olabilir, her biri için bir sınıf açıyoruz. Open Closed Prensibini baz alıyor. İşlem Account işlemi ama account farklı süreçlerini farklı sınıflarda yönetiyoruz.
  public class CloseAccountRequestHandler : IRequestHandler<CloseAccountRequest>
  {

    private readonly IServiceScopeFactory serviceScopeFactory;

    public CloseAccountRequestHandler(IServiceScopeFactory serviceScopeFactory)
    {
      this.serviceScopeFactory = serviceScopeFactory;
    }


    public Task Handle(CloseAccountRequest request, CancellationToken cancellationToken)
    {
      // request.accountType bireysel yada kurumsal olmasına göre  IndividualAccountService veya CorporateAccountService tetikleyecek.

      // service lookup işlemi
      // Domain katmanına routing yapıyor.
      var service = this.serviceScopeFactory.CreateScope().ServiceProvider.GetKeyedService<IAccountService>(request.accountType);

      ArgumentNullException.ThrowIfNull(service);
      service.Close(request.accountNumber, request.closeReason);

      return Task.CompletedTask;

    }
  }
}
