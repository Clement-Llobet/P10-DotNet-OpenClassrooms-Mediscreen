﻿using Mediscreen.API.Routes;
using Mediscreen.Domain.Patient;
using Mediscreen.Domain.Patient.Contracts;
using Mediscreen.Domain.Patient.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mediscreen.API.Endpoints;

public static class PatientEndpoints
{
    public static void MapPatientsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Patient.ListPatients, (IPatientRepository patientRepository) => PatientManager.ListPatients(patientRepository))
            .WithTags(ApiRoutes.Patient.Tag)
            .WithMetadata(ApiRoutes.Patient.ListPatientsMetadata);

        app.MapGet(ApiRoutes.Patient.GetPatient, (IPatientRepository patientRepository, int id) => PatientManager.GetPatient(patientRepository, id))
            .WithTags(ApiRoutes.Patient.Tag)
            .WithMetadata(ApiRoutes.Patient.GetPatientMetadata);

        app.MapPut(ApiRoutes.Patient.UpdatePatient, (IPatientRepository patientRepository, [Bind("Id,FirstName,LastName,BirthDate,Gender,HomeAddress,PhoneNumber")] PatientInput patientInput) => PatientManager.UpdatePatient(patientRepository, patientInput))
            .WithTags(ApiRoutes.Patient.Tag)
            .WithMetadata(ApiRoutes.Patient.UpdatePatientMetadata);

        app.MapPost(ApiRoutes.Patient.CreatePatient, (IPatientRepository patientRepository, [Bind("FirstName,LastName")] PatientInputCreate patientInputCreate) => PatientManager.CreatePatient(patientRepository, patientInputCreate))
            .WithTags(ApiRoutes.Patient.Tag)
            .WithMetadata(ApiRoutes.Patient.CreatePatientMetadata);
    }
}
