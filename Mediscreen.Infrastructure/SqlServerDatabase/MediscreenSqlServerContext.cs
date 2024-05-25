using Mediscreen.Infrastructure.SqlServerDatabase.Entities;
using Mediscreen.Infrastructure.SqlServerDatabase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mediscreen.Infrastructure.SqlServerDatabase;

public class MediscreenSqlServerContext : DbContext
{
    public MediscreenSqlServerContext(DbContextOptions<MediscreenSqlServerContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Patient.Configure(modelBuilder);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}
