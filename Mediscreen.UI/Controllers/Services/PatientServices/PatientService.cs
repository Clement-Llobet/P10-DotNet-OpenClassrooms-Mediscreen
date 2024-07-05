using Mediscreen.Domain.Patient.Dto;
using Mediscreen.UI.Models;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Mediscreen.UI.Controllers.Services.PatientServices;

public class PatientService : IPatientService
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public PatientService(IHttpClientFactory httpClientFactory, IWebHostEnvironment env)
    {
        _client = httpClientFactory.CreateClient("DisableSslValidationHttpClient");
        _baseUrl = GetBaseUrl(env);
    }

    private static string GetBaseUrl(IWebHostEnvironment env)
    {
        return env.IsDevelopment() ? "http://host.docker.internal:65255" : $"https://host.docker.internal:65256";
    }

    public async Task<IEnumerable<PatientViewModel>> GetAllPatients()
    {
        var response = await _client.GetAsync($"{_baseUrl}/api/patients");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<IEnumerable<PatientViewModel>>();

        return content ?? [];
    }

    public async Task<PatientViewModel> GetPatientById(int id)
    {
        var response = await _client.GetAsync($"{_baseUrl}/api/patients/{id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<PatientViewModel>();

        return content ?? throw new InvalidOperationException("Patient not found");
    }

    public async Task<PatientViewModel> UpdatePatient(int id, PatientViewModel patient)
    {
        var patientInput = new PatientInput
        {
            Id = id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Gender = patient.Gender,
            HomeAddress = patient.HomeAddress,
            PhoneNumber = patient.PhoneNumber
        };

        var response = await _client.PutAsJsonAsync($"{_baseUrl}/api/patients/{id}", patientInput);
        response.EnsureSuccessStatusCode();

        return patient;
    }
}
