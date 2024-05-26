using Mediscreen.Infrastructure.SqlServerDatabase.Entities;
using Mediscreen.Infrastructure.SqlServerDatabase.Repositories;
using Mediscreen.Infrastructure.Tools;
using Microsoft.EntityFrameworkCore;

namespace Mediscreen.Infrastructure.SqlServerDatabase;

public class MediscreenSqlServerContext(DbContextOptions<MediscreenSqlServerContext> options) : DbContext(options)
{
    public PatientRepository PatientRepository => new(Set<Patient>());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Patient.Configure(modelBuilder);
        modelBuilder.Entity<Patient>().HasData(new BogusDatasGenerator().GeneratePatient());
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}
