using Mediscreen.UI.Models;

namespace Mediscreen.UI.Controllers.Services.PatientServices;

public class PatientService : IPatientService
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public PatientService(HttpClient client, IWebHostEnvironment env)
    {
        _client = client;
        _baseUrl = GetBaseUrl(env);
    }

    private static string GetBaseUrl(IWebHostEnvironment env)
    {
        return env.IsDevelopment() ? "http://host.docker.internal:5013" : "https://host.docker.internal:7090";
    }

    public async Task<IEnumerable<PatientViewModel>> GetAllPatients()
    {
        var response = await _client.GetAsync($"{_baseUrl}/api/patient");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<IEnumerable<PatientViewModel>>();

        return content ?? [];
    }
}
