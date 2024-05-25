using Mediscreen.Domain.Patient.Contracts;

namespace Mediscreen.Infrastructure.SqlServerDatabase.Repositories;

public class PatientRepository : QueryableRepositoryBase<IPatient>, IPatientRepository
{
    public PatientRepository(IQueryable<IPatient> patients) : base(patients)
    {
    }
}
