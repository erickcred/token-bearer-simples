using JWTBearer.Dtos;
using JWTBearer.Models;
using JWTBearer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWTBearer.Controllers;

[Controller]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
  private readonly ILogger<AuthenticationController> _logger;
  private readonly ITokenService _tokenService;
  private readonly IUserService _userService;

  public AuthenticationController(
    ILogger<AuthenticationController> logger,
    ITokenService tokenService,
    IUserService userService)
  {
    _logger = logger;
    _tokenService = tokenService;
    _userService = userService;
  }

  [HttpPost]
  public IActionResult Login(LoginDto login)
  {
    if (login.UserName.IsNullOrEmpty() || login.Password.IsNullOrEmpty())
    {
      _logger.LogError("Dados para login invalidos, login está nulo");
      return BadRequest(new { error = true, statusCode = StatusCodes.Status400BadRequest, message = "Dados para login invalidos" });
    }
    
    var user = _userService.GetByUserName(login.UserName);
    if (user == null)
    {
      _logger.LogError("Dados para login invalidos, usuario retornou nulo");
      return BadRequest(new { error = true, statusCode = StatusCodes.Status401Unauthorized, message = "Dados para login invalidos" });
    }

    if (user.Password.Equals(login.Password))
      return Ok(new { statusCode = StatusCodes.Status200OK, token = _tokenService.GenerateToken(user) });

    return Unauthorized(new { error = true, statusCode = StatusCodes.Status401Unauthorized, message = "Dados para login invalidos" });
  }
}
