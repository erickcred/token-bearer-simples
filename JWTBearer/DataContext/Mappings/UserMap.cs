using JWTBearer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JWTBearer.DataContext.Mappings
{
  public class UserMap : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> user)
    {
      user.ToTable("User");
      user.HasKey(u => u.Id);
      user.Property(u => u.Id).ValueGeneratedOnAdd();

      user.Property(u => u.UserName)
        .IsRequired()
        .HasColumnName("UserName")
        .HasColumnType("Varchar(100)");

      user.Property(u => u.Password)
        .IsRequired()
        .HasColumnName("Password")
        .HasColumnType("Varchar(255)");

      user.Property(u => u.Role)
        .IsRequired(false)
        .HasColumnName("Role")
        .HasColumnType("Varchar(100)");

    }
  }
}
