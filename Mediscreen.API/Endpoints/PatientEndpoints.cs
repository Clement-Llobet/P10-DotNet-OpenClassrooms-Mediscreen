using Mediscreen.API.Extensions;
using Mediscreen.API.Routes;
using Mediscreen.Domain.Patient;
using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Domain.Patient.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Mediscreen.API.Endpoints;

public static class PatientEndpoints
{
    public static void MapPatientsEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapGet(ApiRoutes.Patient.ListPatients, async (IPatientRepository patientRepository, IDistributedCache cache) =>
        {
            var cacheKey = "ListPatients";
            var patients = await CacheHelper.GetFromCacheAsync<List<PatientOutput>>(cache, cacheKey);

            
            if (patients == null)
            {
                patients = PatientManager.ListPatients(patientRepository, () => patientRepository.ToList());
                await CacheHelper.SetInCacheAsync(cache, cacheKey, patients, CacheOptions.DefaultExpiration);
            }

            return patients;
        }).WithTags(ApiRoutes.Patient.Tag)
          .WithMetadata(ApiRoutes.Patient.ListPatientsMetadata);

        app.MapGet(ApiRoutes.Patient.GetPatient, async (IPatientRepository patientRepository, IDistributedCache cache, int id) =>
        {
            var cacheKey = $"Patient-{id}";
            var patient = await CacheHelper.GetFromCacheAsync<PatientOutput>(cache, cacheKey);

            if (patient == null)
            {
                patient = PatientManager.GetPatient(patientRepository, id);
                await CacheHelper.SetInCacheAsync(cache, cacheKey, patient, CacheOptions.DefaultExpiration);
            }

            return patient;
        }).WithTags(ApiRoutes.Patient.Tag)
          .WithMetadata(ApiRoutes.Patient.GetPatientMetadata);

        app.MapPut(ApiRoutes.Patient.UpdatePatient, (IPatientRepository patientRepository, [Bind("Id,FirstName,LastName,BirthDate,Gender,HomeAddress,PhoneNumber")] PatientInput patientInput) => PatientManager.UpdatePatient(patientRepository, patientInput))
            .WithTags(ApiRoutes.Patient.Tag)
            .WithMetadata(ApiRoutes.Patient.UpdatePatientMetadata);

        app.MapPost(ApiRoutes.Patient.CreatePatient, (IPatientRepository patientRepository, [Bind("FirstName,LastName")] PatientInputCreate patientInputCreate) => PatientManager.CreatePatient(patientRepository, patientInputCreate))
            .WithTags(ApiRoutes.Patient.Tag)
            .WithMetadata(ApiRoutes.Patient.CreatePatientMetadata);
    }
}
