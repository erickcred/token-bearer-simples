using JWTBearer.Models;
using JWTBearer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTBearer.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;

    public UsersController(
      ILogger<UsersController> logger,
      IUserService userService)
    {
      _logger = logger;
      _userService = userService;
    }

    [HttpGet]
    public IActionResult Get()
    {
      var users = _userService.GetUsers();
      return Ok(users);
    }

    [HttpGet("name")]
    public IActionResult GetUserByName(string userName)
    {
      var user = _userService.GetByUserName(userName);
      return Ok(user);
    }

    [HttpPost]
    public IActionResult AddUser(User userModel)
    {
      _userService.Add(userModel);
      return Ok("User Inserted successfully!");
    }

    [HttpPut]
    public IActionResult UpdateUser(User userModel)
    {
      _userService.UpdateUser(userModel);
      return Ok("User Updated successfully!");
    }

    [Authorize(Roles = "admin")]
    [HttpGet("subscribe-certificate")]
    public IActionResult SubscribeCertificate()
    {
      return Ok("ok");
    }

    [Authorize(Roles = "manager, admin")]
    [HttpGet("subscribe-process")]
    public IActionResult SubscribeProcess()
    {
      return Ok("ok");
    }



  }
}
