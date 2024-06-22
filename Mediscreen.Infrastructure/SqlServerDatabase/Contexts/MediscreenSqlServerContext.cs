﻿using Mediscreen.Infrastructure.SqlServerDatabase.Entities;
using Mediscreen.Infrastructure.SqlServerDatabase.Repositories;
using Mediscreen.Infrastructure.Tools;
using Microsoft.EntityFrameworkCore;

namespace Mediscreen.Infrastructure.SqlServerDatabase.Contexts;

public class MediscreenSqlServerContext(DbContextOptions<MediscreenSqlServerContext> options) : DbContext(options)
{
    private readonly MediscreenSqlServerContext _context;
    public PatientRepository PatientRepository => new(Set<Patient>(), _context);

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
