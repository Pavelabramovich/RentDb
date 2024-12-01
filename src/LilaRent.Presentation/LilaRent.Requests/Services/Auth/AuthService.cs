using LilaRent.Application.Dto;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Net;


namespace LilaRent.Requests.Services;


public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly string _serverUrl;


    public AuthService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        _serverUrl = configuration["UriData:ApiUri"]
            ?? throw new ArgumentException("There is no UriData:ApiUri in configuration");
    }


    public async Task<TokensDto> LoginAsync(CredentialsDto credentials)
    {
        var endpoint = $"{_serverUrl}/auth/login";

        var json = JsonSerializer.Serialize(credentials);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
        else
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            // var json = await response.Content.ReadAsStringAsync();

            var tokens = await response.Content.ReadFromJsonAsync<TokensDto>(options)
                ?? throw new InvalidCastException("Can not cast responce to tokens");

            return tokens;
        }
    }

    public async Task<TokensDto> RefreshAsync(TokensDto tokens)
    {
        var endpoint = $"{_serverUrl}/auth/refresh";

        var json = JsonSerializer.Serialize(tokens);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
        else
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            // var json = await response.Content.ReadAsStringAsync();

            var newTokens = await response.Content.ReadFromJsonAsync<TokensDto>(options)
                ?? throw new InvalidCastException("Can not cast responce to tokens");

            return newTokens;
        }
    }
}
