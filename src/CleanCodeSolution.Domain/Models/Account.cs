using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeSolution.Domain.Models
{
  public enum AccountStatus
  {
    Closed = 100,
    Open = 120
  }

  // Banka Müşteri Hesap
  public class Account
  {
    public Guid AccountId { get; init; }
    public string AccountNumber { get; init; }
    public DateTime CreatedAt { get; init; }
    public AccountStatus Status { get; private set; }

    public string CloseReason { get; private set; }

    public bool Closed { get; private set; }

    public DateTime? ClosedAt { get; private set; }

    private Account(string accountNumber)
    {
      ArgumentException.ThrowIfNullOrEmpty(accountNumber);
      AccountNumber = accountNumber;
      AccountId = Guid.NewGuid();
      CreatedAt = DateTime.Now;
      Status = AccountStatus.Open;
    }

    /// <summary>
    /// Hesap Açılışı
    /// </summary>
    /// <param name="accountNumber"></param>
    /// <returns></returns>
    public static Account Create(string accountNumber)
    {
      return new Account(accountNumber);
    }

    /// <summary>
    /// Hesap kapanışı 
    /// </summary>
    /// <param name="closeReason"></param>
    public void Close(string closeReason)
    {
      ArgumentNullException.ThrowIfNullOrEmpty(closeReason);
      CloseReason = closeReason;
      ClosedAt = DateTime.Now;
      Closed = true;
      Status = AccountStatus.Closed;
    }


  }
}
