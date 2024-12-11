using CleanCodeSolution.Domain.Events;
using CleanCodeSolution.Domain.Models;
using CleanCodeSolution.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeSolution.Domain.Services
{
  // Cohesion : Birbileri ile alakalı nesnelerin bir uyumluluk içerisinde birbirleri ile bir düzen dahilinde çalışması . High Cohesion => Yüksek Uyumluluk => Kod Kalite Standartı

  // AccountRepo => Account => AccountClose işlemi için Bildirim tetikleme süreci gibi.
  public class IndividualAccountService: IAccountService
  {
    // Dependecy Inversion Principle (Katmanlar arasında zayıf bağlılık)
    private readonly IAccountRepo accountRepo;
    private readonly IMediator mediator;

        public IndividualAccountService()
        {
            
        }

        public IndividualAccountService(IAccountRepo accountRepo, IMediator mediator)
    {
      this.accountRepo = accountRepo;
      this.mediator = mediator;
    }

    // Single Responsibility
    public void Close(string accountNumber, string closeReason)
    {
      Console.Out.WriteLine("Bireysel Hesap Kapanış");

      var account = accountRepo.FindByAccountNumber(accountNumber);
      ArgumentNullException.ThrowIfNull(account);

      account.Close(closeReason);
      accountRepo.Save(account);


      var @event = new AccountClosed(accountNumber);
      mediator.Publish(@event); // sürecin handler devredilmesi

    }


  }
}
