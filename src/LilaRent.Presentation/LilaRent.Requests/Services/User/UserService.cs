using LilaRent.Application.Dto;
using LilaRent.Domain.Fields;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using CreateModel = LilaRent.Requests.RequestModels.UserWithProfileCreatingRequestModel;
using System.Net.Http.Json;


namespace LilaRent.Requests.Services;


public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly string _serverUrl;


    public UserService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        _serverUrl = configuration["UriData:ApiUri"] 
            ?? throw new ArgumentException("There is no UriData:ApiUri in configuration");
    }


    public async Task AddWithProfileAsync(UserWithProfileCreatingDto dto)
    {
        string endpoint = $"{_serverUrl}/users";

        using var formData = new MultipartFormDataContent()
        {
            { new StringContent(dto.Login), nameof(CreateModel.Login) },
            { new StringContent(dto.Password), nameof(CreateModel.Password) },
            { new StringContent(dto.Profile.LegalEntityType.ToString()), nameof(CreateModel.LegalEntityType) },
            { new StringContent(dto.Profile.Name), nameof(CreateModel.Name) },
            { new StringContent(dto.Profile.Phone), nameof(CreateModel.Phone) },
            { new StringContent(dto.Profile.Email), nameof(CreateModel.Email) },
            { new StringContent(dto.Profile.Description), nameof(CreateModel.Description) },
        };

        var image = dto.Profile.Image;
        var imageContent = new StreamContent(image.Stream);
        var extension = image.Format.Extension;
        imageContent.Headers.ContentType = new MediaTypeHeaderValue(GetContentType(extension)); 
        formData.Add(imageContent, nameof(CreateModel.Image), Path.GetFileName($"image{extension}"));


        var response = await _httpClient.PostAsync(endpoint, formData);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }


    public async Task<ProfileSummaryDto> GetFirstProfileIdAsycn(string login, CancellationToken cancellationToken = default)
    {
        var endpoint = $"{_serverUrl}/profiles/of-{login}";

        var response = await _httpClient.GetAsync(endpoint, cancellationToken);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        // var json = await response.Content.ReadAsStringAsync();

        var profileDto = await response.Content.ReadFromJsonAsync<ProfileSummaryDto>(options)
            ?? throw new InvalidCastException("Can not cast responce to profile summary dto");

        return profileDto;
    }

    public async Task<LegalPersonProfileDto> GetLegalPersonProfile(Guid id, CancellationToken cancellationToken = default)
    {
        var endpoint = $"{_serverUrl}/profiles/owner/{id}";

        var response = await _httpClient.GetAsync(endpoint, cancellationToken);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        // var json = await response.Content.ReadAsStringAsync();

        var profileDto = await response.Content.ReadFromJsonAsync<LegalPersonProfileDto>(options)
            ?? throw new InvalidCastException("Can not cast responce to profile summary dto");

        return profileDto;
    }


    private static string GetContentType(string extension)
    {
        var provider = new FileExtensionContentTypeProvider();

        if (provider.TryGetContentType(extension, out string? contentType))
        {
            return contentType;
        }

        return "application/octet-stream";
    }
}
