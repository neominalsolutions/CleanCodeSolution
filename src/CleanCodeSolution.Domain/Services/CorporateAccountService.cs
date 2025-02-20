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
  // Open Closed örneği
  public class CorporateAccountService : IAccountService
  {
    private readonly IAccountRepo accountRepo;
    private readonly IMediator mediator;

    public CorporateAccountService(IAccountRepo accountRepo, IMediator mediator)
    {
      this.accountRepo = accountRepo;
      this.mediator = mediator;
    }

    public void Close(string accountNumber, string closeReason)
    {
      //Account.Create("4e324324");

      Console.Out.WriteLine("Kurumsal Hesap Kapanış");
      this.mediator.Publish(new AccountClosed("3432432"));

    }
  }
}
