using CleanCodeSolution.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeSolution.Domain.Handlers
{
  // AccountClosedHandler tek değişim sebebi hesabının kapatılmasını takip etmek.
  // Single Responsibility
  // Handler AccountClosed eventini dinler => Event Driven Development 
  public class AccountClosedHandler : INotificationHandler<AccountClosed>
  {
    // Bildirim Mail gönderme vs Hesap kapatılma sonrası işlenecek bir Policy (Poliçe) var bu kod üzerinden devam edicek.
    public Task Handle(AccountClosed notification, CancellationToken cancellationToken)
    {
      Console.Out.WriteLine($"{notification.accountNumber} nolu Hesap Kapatıldı.");
      return Task.CompletedTask;
    }
  }
}
