using CleanCodeSolution.Application.Requests;
using CleanCodeSolution.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanCodeAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountsController : ControllerBase
  {
    private readonly IMediator mediator;


    public AccountsController(IMediator mediator)
    {
      this.mediator = mediator;
    }

    [HttpPost]
    public IActionResult CloseAccount([FromBody] CloseAccountRequest request)
    {
      this.mediator.Send(request); // CloseAccountRequestHandler isteğin yönlendirilmesini sağlar.

      return Ok();
    }
  }
}
