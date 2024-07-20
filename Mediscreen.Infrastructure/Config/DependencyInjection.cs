using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Infrastructure.MongoDbDatabase.Repository;
using Mediscreen.Infrastructure.SqlServerDatabase.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Mediscreen.Infrastructure.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddSqlServerDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MediscreenSqlServerContext>(options =>
        {
            options.UseSqlServer(connectionString, (o) => o.EnableRetryOnFailure());
            options.EnableSensitiveDataLogging(true);
        });

        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseSqlServer(connectionString, (o) => o.EnableRetryOnFailure());
        });

        services.AddScoped<IPatientRepository>(provider => provider.GetRequiredService<MediscreenSqlServerContext>().PatientRepository);

        return services;
    }

    public static IServiceCollection AddMongoDbDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton(new MongoClient(connectionString));

        services.AddScoped<INotesRepository>(provider => new NotesRepository(provider.GetRequiredService<MongoClient>()));

        return services;
    }
}
