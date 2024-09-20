using Mediscreen.Domain.Note.Contracts;
using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Domain.Triggers.Contracts;
using Mediscreen.Infrastructure.MongoDbDatabase.Documents;
using Mediscreen.Infrastructure.MongoDbDatabase.Repository;
using Mediscreen.Infrastructure.SqlServerDatabase.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Mediscreen.Infrastructure.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddSqlServerDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextFactory<MediscreenSqlServerContext>(options =>
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

    public static IServiceCollection AddMongoDbDatabase(this IServiceCollection services, string mongoConnectionString)
    {
        services.AddScoped<IMongoClient, MongoClient>(sp => new MongoClient(mongoConnectionString));
        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var database = client.GetDatabase("Mediscreen");
            var collection = database.GetCollection<Notes>("Notes");
            return collection.AsQueryable().OfType<INotes>();
        });
        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var database = client.GetDatabase("Mediscreen");
            var collection = database.GetCollection<Triggers>("Triggers");
            return collection.AsQueryable().OfType<ITriggers>();
        });

        services.AddSingleton(new MongoClient(mongoConnectionString));

        services.AddTransient<INotesRepository, NotesRepository>();
        services.AddTransient<ITriggersRepository, TriggersRepository>();

        return services;
    }
}
