using JWTBearer.DataContext;
using JWTBearer.Models;
using JWTBearer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JWTBearer.Repositories;

public class UserRepository : IUserRepository
{
  private readonly UserContext _userContext;

  public UserRepository(
    UserContext userContext)
  {
    _userContext = userContext;
  }

  public IEnumerable<User> GetUsers()
  {
    return _userContext.Users.AsNoTracking().ToList();
  }

  public void Add(User user)
  {
    _userContext.Users.Add(user);
    _userContext.SaveChanges();
  }

  public void UpdateUser(User user)
  {
    _userContext.Users.Update(user);
    _userContext.SaveChanges();
  }

  public User? GetByUserName(string userName)
  {
    var user = _userContext.Users.AsNoTracking().FirstOrDefault(u => u.UserName.Equals(userName));
    return user;
  }

}
