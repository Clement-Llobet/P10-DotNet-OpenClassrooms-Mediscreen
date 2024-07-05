using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Domain.Patient.Dto;
using Mediscreen.Infrastructure.SqlServerDatabase.Contexts;
using Mediscreen.Infrastructure.SqlServerDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mediscreen.Infrastructure.SqlServerDatabase.Repositories;

public class PatientRepository : QueryableRepositoryBase<IPatient>, IPatientRepository
{
    private readonly MediscreenSqlServerContext _dbContext;

    public PatientRepository(IQueryable<IPatient> patients, MediscreenSqlServerContext dbContext) : base(patients)
    {
        _dbContext = dbContext;
    }

    public Task<int> UpdatePatient(PatientInput patientInput)
    {
        var patient = _dbContext.PatientRepository.First(x => x.Id == patientInput.Id);

        patient.FirstName = patientInput.FirstName ?? patient.FirstName;
        patient.LastName = patientInput.LastName ?? patient.LastName;
        patient.BirthDate = patientInput.BirthDate ?? patient.BirthDate;
        patient.Gender = patientInput.Gender ?? patient.Gender;
        patient.HomeAddress = patientInput.HomeAddress ?? patient.HomeAddress;
        patient.PhoneNumber = patientInput.PhoneNumber ?? patient.PhoneNumber;

        _dbContext.Update(patient);
        _dbContext.SaveChanges();

       return Task.FromResult(patient.Id);
    }

    public Task<int> CreatePatient(PatientInputCreate patientInputCreate)
    {
        var patient = new Patient
        {
            FirstName = patientInputCreate.FirstName,
            LastName = patientInputCreate.LastName,
            BirthDate = patientInputCreate.BirthDate,
            Gender = patientInputCreate.Gender,
            HomeAddress = patientInputCreate.HomeAddress ?? "",
            PhoneNumber = patientInputCreate.PhoneNumber ?? ""
        };

        _dbContext.Add(patient);
        _dbContext.SaveChanges();

        return Task.FromResult(patient.Id);
    }
}
