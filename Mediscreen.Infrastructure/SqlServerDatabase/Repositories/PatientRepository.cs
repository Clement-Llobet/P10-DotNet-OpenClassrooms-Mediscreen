using Mediscreen.Domain.Patient.Contracts;
using System.Collections;
using System.Linq.Expressions;

namespace Mediscreen.Infrastructure.SqlServerDatabase.Repositories;

public class PatientRepository : QueryableRepositoryBase<IPatient>, IPatientRepository
{
    public PatientRepository(IQueryable<IPatient> patients) : base(patients)
    {
    }
}
