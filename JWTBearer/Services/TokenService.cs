using JWTBearer.Models;
using JWTBearer.Repositories.Interfaces;
using JWTBearer.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTBearer.Services;

public class TokenService : ITokenService
{
  private readonly IConfiguration _config;
  private readonly IUserRepository _userRespository;

  public TokenService(
    IConfiguration config,
    IUserRepository userRepository)
  {
    _config = config;
    _userRespository = userRepository;
  }

  public string GenerateToken(User user)
  {
    var userDataBase = _userRespository.GetByUserName(user.UserName);
    if (user.UserName != userDataBase.UserName || user.Password != userDataBase.Password)
      return string.Empty;

    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT")["Key"] ?? string.Empty));
    var issuer = _config.GetSection("JWT")["Issuer"];
    var audience = _config.GetSection("JWT")["Audience"];

    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
    var tokenOptions = new JwtSecurityToken(
      issuer: issuer,
      audience: audience,
      claims: new[]
      {
        new Claim(ClaimTypes.Name, userDataBase.UserName),
        new Claim(ClaimTypes.Role, userDataBase.Role)
      },
      expires: DateTime.Now.AddHours(10),
      signingCredentials: signingCredentials
    );

    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

    return token;
  }
}
