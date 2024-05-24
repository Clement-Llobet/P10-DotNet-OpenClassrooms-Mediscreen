using Mediscreen.Infrastructure.SqlServerDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mediscreen.Infrastructure.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddSqlServerDatabase(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<MediscreenSqlServerContext>(options =>
                   options.UseSqlServer(connectionString));
    }
}
