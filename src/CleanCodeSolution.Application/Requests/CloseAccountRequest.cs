using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeSolution.Application.Requests
{
  // Request Nesneleride Immutable çalışır. Bu sebeple record olarak tanımlıyoruz.
  // Command nesnelerinde ise emir kipi kullanırız. Use-Case'ler application katmanında request olarak yazılır.
  // Data Structures => Data Transfer Object
  public record CloseAccountRequest([Required] string accountNumber,string closeReason, string accountType):IRequest;
 
}
