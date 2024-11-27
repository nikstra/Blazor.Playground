using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebUi.Models;
using Microsoft.Extensions.Options;

namespace WebUi.Clients;

public class HolidaysClient
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    public HolidaysClient(HttpClient client, IOptions<JsonSerializerOptions> options)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://date.nager.at");
        _client.Timeout = new TimeSpan(0, 0, 30);
        _client.DefaultRequestHeaders.Clear();

        _options = options.Value;
    }

    public async Task<IEnumerable<PublicHolidayModel>> GetHolidays(int year, string countryCode)
    {
        var path = $"/api/v3/PublicHolidays/{year}/{countryCode}";
        using var response = await _client.GetAsync(path, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var holidays = await JsonSerializer.DeserializeAsync<IEnumerable<PublicHolidayModel>>(stream, _options);

        return holidays ?? Enumerable.Empty<PublicHolidayModel>();
    }
}