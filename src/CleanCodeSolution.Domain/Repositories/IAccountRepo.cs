using CleanCodeSolution.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeSolution.Domain.Repositories
{
  public interface IAccountRepo
  {
    Account FindByAccountNumber(string accountNumber);
    void Save(Account account);
  }
}
