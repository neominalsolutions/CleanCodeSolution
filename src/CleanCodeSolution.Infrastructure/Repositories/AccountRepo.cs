using CleanCodeSolution.Domain.Models;
using CleanCodeSolution.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeSolution.Infrastructure.Repositories
{
  public class AccountRepo : IAccountRepo
  {
    public Account FindByAccountNumber(string accountNumber)
    {
      return Account.Create(accountNumber);
    }

    public void Save(Account account)
    {
      Console.Out.WriteLine($"{account.AccountNumber} nolu account kayıt edildi");
    }
  }
}
