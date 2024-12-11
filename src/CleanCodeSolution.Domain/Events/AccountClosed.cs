using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeSolution.Domain.Events
{
  // INotification interface sayesinde event olduğunu anladık
  // AccountClosed dili geçmiş zaman kipinde kullanılır
  // Eventler Immutable çalışır bu sebeple record olarak tanımladık.
  // Data Transfer Object => Data Structure olarak kullandık.
  public record AccountClosed(string accountNumber):INotification;
  
}
