using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Infrastructure.SqlServerDatabase.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
}
