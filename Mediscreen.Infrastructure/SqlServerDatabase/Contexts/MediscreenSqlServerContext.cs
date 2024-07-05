using Mediscreen.Infrastructure.SqlServerDatabase.Entities;
using Mediscreen.Infrastructure.SqlServerDatabase.Repositories;
using Mediscreen.Infrastructure.Tools;
using Microsoft.EntityFrameworkCore;

namespace Mediscreen.Infrastructure.SqlServerDatabase.Contexts;

public class MediscreenSqlServerContext : DbContext
{
    public MediscreenSqlServerContext(DbContextOptions<MediscreenSqlServerContext> options) : base(options)
    {
        
    }
    public PatientRepository PatientRepository => new(Set<Patient>(), this);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Patient.Configure(modelBuilder);

        var bogusDatasGenerator = new BogusDatasGenerator();

        modelBuilder.Entity<Patient>().HasData(bogusDatasGenerator.Patients);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}
