using Microsoft.EntityFrameworkCore;

namespace Mediscreen.Infrastructure.SqlServerDatabase.Entities;

public class Patient
{
    public int Id { get; set; }
    public required string FirstName { get; set; } = string.Empty;
    public required string LastName { get; set; } = string.Empty;
    public required DateTime BirthDate { get; set; }
    public required string Gender { get; set; } = string.Empty;
    public required string HomeAddress { get; set; } = string.Empty;
    public required string PhoneNumber { get; set; } = string.Empty;

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable(nameof(Patient));
        });
    }
}
