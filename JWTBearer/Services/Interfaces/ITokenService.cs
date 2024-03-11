using JWTBearer.Models;

namespace JWTBearer.Services.Interfaces;

public interface ITokenService
{
  string GenerateToken(User user);
}
