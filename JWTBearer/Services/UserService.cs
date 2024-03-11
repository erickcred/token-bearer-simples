using JWTBearer.Models;
using JWTBearer.Repositories.Interfaces;
using JWTBearer.Services.Interfaces;

namespace JWTBearer.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public IEnumerable<User> GetUsers()
  {
    return _userRepository.GetUsers();
  }

  public void Add(User user)
  {
    _userRepository.Add(user);
  }

  public void UpdateUser(User user)
  {
    _userRepository.UpdateUser(user);
  }

  public User? GetByUserName(string userName)
  {
    return _userRepository.GetByUserName(userName);
  }
}
