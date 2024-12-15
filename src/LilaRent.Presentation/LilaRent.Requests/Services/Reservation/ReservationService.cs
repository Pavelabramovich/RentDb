using LilaRent.Application.Dto;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http.Json;


namespace LilaRent.Requests.Services;


public class ReservationService : IReservationService
{
    private readonly HttpClient _httpClient;
    private readonly string _serverUrl;


    public ReservationService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        _serverUrl = configuration["UriData:ApiUri"]
            ?? throw new ArgumentException("There is no UriData:ApiUri in configuration");
    }


    public async Task<IEnumerable<AnnouncementSummaryDto>> GetPrevios(Guid id, CancellationToken cancellationToken = default)
    {
        var endpoint = $"{_serverUrl}/profiles/previos/{id}";

        var response = await _httpClient.GetAsync(endpoint, cancellationToken);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        // var json = await response.Content.ReadAsStringAsync();

        var profileDto = await response.Content.ReadFromJsonAsync<IEnumerable<AnnouncementSummaryDto>>(options)
            ?? throw new InvalidCastException("Can not cast responce to announcement summary dto");

        return profileDto;
    }
}
