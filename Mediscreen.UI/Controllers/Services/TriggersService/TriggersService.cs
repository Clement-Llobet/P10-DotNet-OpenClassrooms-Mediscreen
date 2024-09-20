using Mediscreen.UI.Models;

namespace Mediscreen.UI.Controllers.Services.TriggersService;

public class TriggersService : ITriggersService
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public TriggersService(IHttpClientFactory httpClientFactory, IWebHostEnvironment env)
    {
        _client = httpClientFactory.CreateClient("DisableSslValidationHttpClient");
        _baseUrl = GetBaseUrl(env);
    }

    private static string GetBaseUrl(IWebHostEnvironment env)
    {
        return env.IsDevelopment() ? "http://host.docker.internal:65255" : $"https://host.docker.internal:65256";
    }

    public async Task<IEnumerable<TriggerViewModel>> GetAllTriggers()
    {
        var response = await _client.GetAsync($"{_baseUrl}/api/triggers");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<IEnumerable<TriggerViewModel>>();
        return content ?? [];
    }
}
