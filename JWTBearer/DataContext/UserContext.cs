using JWTBearer.DataContext.Mappings;
using JWTBearer.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTBearer.DataContext;

public class UserContext : DbContext
{
  public UserContext(DbContextOptions<UserContext> options) : base(options) { }

  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfiguration(new UserMap());
  }
}
