using CleanCodeAPI.Attributes;
using CleanCodeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanCodeAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TestsController : ControllerBase
  {
    private readonly ITestService testService;

    public TestsController(ITestService testService)
    {
      this.testService = testService;
    }


    // RequestInterceptionAttribute attributeler başka servisleri kendi içlerinde enjekte ediyorsa service filter attribute den yaralanırız.
    // [ServiceFilter(typeof(RequestInterceptionAttribute))]
    //[Authorize(Roles = "Admin")]
    // [RequestInterception]
    [HttpGet]
    public IActionResult Get()
    {
      this.testService.Handle();

      return Ok();
    }


    [ServiceFilter(typeof(RequestInterceptionAttribute))]
    [HttpGet("{id}")]
    //[Authorize] 
    public IActionResult GetById(int id)
    {
      return Ok();
    }
  }
}
