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
    public void Close(string accountNumber, string closeReason)
    {
      Console.Out.WriteLine("Kurumsal Hesap Kapanış");
    }
  }
}
