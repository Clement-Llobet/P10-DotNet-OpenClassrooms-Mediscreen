using Microsoft.EntityFrameworkCore;

namespace Mediscreen.Infrastructure.SqlServerDatabase;

public class MediscreenSqlServerContext : DbContext
{
    public MediscreenSqlServerContext(DbContextOptions<MediscreenSqlServerContext> options) : base(options)
    {

    }
}
