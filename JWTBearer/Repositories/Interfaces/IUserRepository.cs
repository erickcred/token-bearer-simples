using JWTBearer.Models;

namespace JWTBearer.Repositories.Interfaces;

public interface IUserRepository
{
  IEnumerable<User> GetUsers();
  void Add(User user);
  void UpdateUser(User user);
  User GetByUserName(string userName);
}
