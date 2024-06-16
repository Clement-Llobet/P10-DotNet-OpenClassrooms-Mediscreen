using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mediscreen.Infrastructure.SqlServerDatabase.Entities;

public class User : IdentityUser
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable(nameof(User));
        });
    }
};

