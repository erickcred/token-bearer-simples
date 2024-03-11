using JWTBearer.Models;

namespace JWTBearer.Services.Interfaces;

public interface IUserService
{
  IEnumerable<User> GetUsers();
  void Add(User user);
  void UpdateUser(User user);
  User GetByUserName(string userName);
}
