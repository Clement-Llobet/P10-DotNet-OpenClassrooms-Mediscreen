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
}
