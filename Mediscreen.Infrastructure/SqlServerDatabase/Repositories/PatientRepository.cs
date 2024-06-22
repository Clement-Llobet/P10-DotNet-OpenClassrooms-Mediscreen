using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Domain.Patient.Dto;
using Mediscreen.Infrastructure.SqlServerDatabase.Contexts;

namespace Mediscreen.Infrastructure.SqlServerDatabase.Repositories;

public class PatientRepository : QueryableRepositoryBase<IPatient>, IPatientRepository
{
    private readonly MediscreenSqlServerContext _dbContext;

    public PatientRepository(IQueryable<IPatient> patients, MediscreenSqlServerContext dbContext) : base(patients)
    {
        _dbContext = dbContext;
    }

    public int UpdatePatient(PatientInput patientInput)
    {
        var patient = _dbContext.PatientRepository.FirstOrDefault(x => x.Id == patientInput.Id);

        patient!.FirstName = patientInput.FirstName ?? patient.FirstName;
        patient.LastName = patientInput.LastName ?? patient.LastName;
        patient.BirthDate = patientInput.BirthDate ?? patient.BirthDate;
        patient.Gender = patientInput.Gender ?? patient.Gender;
        patient.HomeAddress = patientInput.HomeAddress ?? patient.HomeAddress;
        patient.PhoneNumber = patientInput.PhoneNumber ?? patient.PhoneNumber;

        var patientUpdated = _dbContext.Update(patient);
        _dbContext.SaveChanges();

        return patientUpdated.Entity.Id;
    }
}
