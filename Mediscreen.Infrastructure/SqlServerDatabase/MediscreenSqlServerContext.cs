using Mediscreen.Infrastructure.SqlServerDatabase.Entities;
using Mediscreen.Infrastructure.SqlServerDatabase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mediscreen.Infrastructure.SqlServerDatabase;

public class MediscreenSqlServerContext(DbContextOptions<MediscreenSqlServerContext> options) : DbContext(options)
{
    public PatientRepository PatientRepository => new(Set<Patient>());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Patient.Configure(modelBuilder);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}
