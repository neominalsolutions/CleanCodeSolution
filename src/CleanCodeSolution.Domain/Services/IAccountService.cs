using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeSolution.Domain.Services
{
  public interface IAccountService
  {
    void Close(string accountNumber, string closeReason);
  }
}
