using LilaRent.Application.Dto;
using LilaRent.Domain.Fields;
using LilaRent.Requests.RequestModels;
using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using CreateModel = LilaRent.Requests.RequestModels.AnnouncementCreatingRequestModel;
using UpdateModel = LilaRent.Requests.RequestModels.AnnouncementUpdatingRequestModel;
using System.Text;
using System;


namespace LilaRent.Requests.Services;


public class AnnouncementService : IAnnouncementService
{
    private readonly HttpClient _httpClient;
    private readonly string _serverUrl;


    public AnnouncementService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        _serverUrl = configuration["UriData:ApiUri"] 
            ?? throw new ArgumentException("There is no UriData:ApiUri in configuration");
    }

    public async Task<IEnumerable<AnnouncementSummaryDto>> GetAllAsync()
    {
        string endpoint = $"{_serverUrl}/announcements";

        var response = await _httpClient.GetAsync(endpoint);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        // var json = await response.Content.ReadAsStringAsync();

        var announcements = await response.Content.ReadFromJsonAsync<IEnumerable<AnnouncementSummaryDto>>(options)
            ?? throw new InvalidCastException("Can not cast responce to announcements");

        return announcements;
    }


    public async Task<IEnumerable<ReservationSummaryDto>> GetReservations(Guid announcementId)
    {
        var endpoint = $"{_serverUrl}/announcements/{announcementId}/reservations";

        var response = await _httpClient.GetAsync(endpoint);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

         var json = await response.Content.ReadAsStringAsync();

        var announcements = await response.Content.ReadFromJsonAsync<IEnumerable<ReservationSummaryDto>>(options)
            ?? throw new InvalidCastException("Can not cast responce to announcements");

        return announcements;
    }

    public async Task<AnnouncementUpdatingDetailsDto> GetByIdAsync(Guid id)
    {
        try
        {
            string endpoint = $"{_serverUrl}/announcements/{id}";

            var response = await _httpClient.GetAsync(endpoint);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var json = await response.Content.ReadAsStringAsync();

            var dto = await response.Content.ReadFromJsonAsync<AnnouncementUpdatingDetailsDto>(options)
                ?? throw new InvalidCastException("Can not cast responce to announcement detatils dto");

            return dto;
        }
        catch (Exception ex)
        {
            ;
            throw;
        }
    }

    public async Task<AnnouncementDetailsDto> GetById2Async(Guid id)
    {
        try
        {
            string endpoint = $"{_serverUrl}/announcements/2/{id}";

            var response = await _httpClient.GetAsync(endpoint);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var json = await response.Content.ReadAsStringAsync();

            var dto = await response.Content.ReadFromJsonAsync<AnnouncementDetailsDto>(options)
                ?? throw new InvalidCastException("Can not cast responce to announcement detatils dto");

            return dto;
        }
        catch (Exception ex)
        {
            ;
            throw;
        }
    }

    public async Task<IEnumerable<AnnouncementSummaryDto>> GetByProfileIdAsync(Guid profileId, string accessToken)
    {
        var endpoint = $"{_serverUrl}/announcements/profile/{profileId}";

        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.SendAsync(request);


        //var response = await _httpClient.GetAsync(endpoint);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        // var json = await response.Content.ReadAsStringAsync();

        var announcements = await response.Content.ReadFromJsonAsync<IEnumerable<AnnouncementSummaryDto>>(options)
            ?? throw new InvalidCastException("Can not cast responce to announcements");

        return announcements;
    }


    public async Task AddAsync(AnnouncementCreatingDto dto)
    {
        string endpoint = $"{_serverUrl}/announcements";

        using var formData = new MultipartFormDataContent()
        {
            { new StringContent(dto.ProfileId.ToString()), nameof(CreateModel.ProfileId) },
            { new StringContent(dto.RentObjectName), nameof(CreateModel.RentObjectName) },
            { new StringContent(dto.Address), nameof(CreateModel.Address) },
            { new StringContent(dto.Description), nameof(CreateModel.Description) },
            { new StringContent(dto.Price.Value.ToString()), $"{nameof(CreateModel.Price)}.{nameof(Price.Value)}" },
            { new StringContent(dto.Price.TimeUnit.ToString()), $"{nameof(CreateModel.Price)}.{nameof(Price.TimeUnit)}" },
            { new StringContent(dto.IsPromoted.ToString()), nameof(CreateModel.IsPromoted) },
            { new StringContent(dto.OpenTime.ToString("HH:mm")), nameof(CreateModel.OpenTime) },
            { new StringContent(dto.CloseTime.ToString("HH:mm")), nameof(CreateModel.CloseTime) },
            { new StringContent(dto.BreakBetweenReservations.ToString()), nameof(CreateModel.BreakBetweenReservations) },
            { new StringContent(dto.CanClientsChangeRecords.ToString()), nameof(CreateModel.CanClientsChangeRecords) },
            { new StringContent(dto.CanClientsDisableRecords.ToString()), nameof(CreateModel.CanClientsDisableRecords) },
            { new StringContent(dto.UseDiscount.ToString()), nameof(CreateModel.UseDiscount) },
        };

        if (dto.MinReservationTime is not null)
            formData.Add(new StringContent(dto.MinReservationTime.ToString()!), nameof(CreateModel.MinReservationTime));

        if (dto.MaxReservationTime is not null)
            formData.Add(new StringContent(dto.MaxReservationTime.ToString()!), nameof(CreateModel.MaxReservationTime));

        if (dto.MinTimeForDiscount is not null)
            formData.Add(new StringContent(dto.MinTimeForDiscount.ToString()!), nameof(CreateModel.MinTimeForDiscount));

        if (dto.MaxTimeForDiscount is not null)
            formData.Add(new StringContent(dto.MaxTimeForDiscount.ToString()!), nameof(CreateModel.MaxTimeForDiscount));

        if (dto.DiscountPercentage is not null)
            formData.Add(new StringContent(dto.DiscountPercentage.ToString()!), nameof(CreateModel.DiscountPercentage));

        int i = 0;

        foreach (var image in dto.Images)
        {
            var streamContent = new StreamContent(image.Stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(GetContentType(image.Format.Extension)); 
            formData.Add(streamContent, nameof(CreateModel.Images), Path.GetFileName($"image{i}{image.Format}"));
        }

        var offerAgreement = dto.OfferAgreement;
        var offerContent = new StreamContent(offerAgreement.Stream);
        var extension = offerAgreement.Format.Extension;
        offerContent.Headers.ContentType = new MediaTypeHeaderValue(GetContentType(extension)); 
        formData.Add(offerContent, nameof(CreateModel.OfferAgreement), Path.GetFileName($"offer_agreement{extension}"));


        var response = await _httpClient.PostAsync(endpoint, formData);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }


    public async Task UpdateAsync(AnnouncementUpdatingDto dto)
    {
        string endpoint = $"{_serverUrl}/announcements";

        using var formData = new MultipartFormDataContent()
        {
            { new StringContent(dto.Id.ToString()), nameof(UpdateModel.Id) },
            { new StringContent(dto.RentObjectName), nameof(UpdateModel.RentObjectName) },
            { new StringContent(dto.Address), nameof(UpdateModel.Address) },
            { new StringContent(dto.Description), nameof(UpdateModel.Description) },
            { new StringContent(dto.Price.Value.ToString()), $"{nameof(UpdateModel.Price)}.{nameof(Price.Value)}" },
            { new StringContent(dto.Price.TimeUnit.ToString()), $"{nameof(UpdateModel.Price)}.{nameof(Price.TimeUnit)}" },
            { new StringContent(dto.IsPromoted.ToString()), nameof(UpdateModel.IsPromoted) },
            { new StringContent(dto.OpenTime.ToString("HH:mm")), nameof(UpdateModel.OpenTime) },
            { new StringContent(dto.CloseTime.ToString("HH:mm")), nameof(UpdateModel.CloseTime) },
            { new StringContent(dto.BreakBetweenReservations.ToString()), nameof(UpdateModel.BreakBetweenReservations) },
            { new StringContent(dto.CanClientsChangeRecords.ToString()), nameof(UpdateModel.CanClientsChangeRecords) },
            { new StringContent(dto.CanClientsDisableRecords.ToString()), nameof(UpdateModel.CanClientsDisableRecords) },
            { new StringContent(dto.UseDiscount.ToString()), nameof(UpdateModel.UseDiscount) },
        };

      //  if (dto.MinReservationTime is not null)
            formData.Add(new StringContent((dto.MinReservationTime ?? TimeSpan.Zero).ToString()!), nameof(UpdateModel.MinReservationTime));

        //if (dto.MaxReservationTime is not null)
            formData.Add(new StringContent((dto.MaxReservationTime ?? TimeSpan.Zero).ToString()!), nameof(UpdateModel.MaxReservationTime));

        if (dto.MinTimeForDiscount is not null)
            formData.Add(new StringContent(dto.MinTimeForDiscount.ToString()!), nameof(UpdateModel.MinTimeForDiscount));

        if (dto.MaxTimeForDiscount is not null)
            formData.Add(new StringContent(dto.MaxTimeForDiscount.ToString()!), nameof(UpdateModel.MaxTimeForDiscount));

        if (dto.DiscountPercentage is not null)
            formData.Add(new StringContent(dto.DiscountPercentage.ToString()!), nameof(UpdateModel.DiscountPercentage));


        int i = 0;

        foreach (var newImage in dto.NewImages)
        {
            var streamContent = new StreamContent(newImage.Stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(GetContentType(newImage.Format.Extension));

            formData.Add(
                streamContent,
                $"{nameof(UpdateModel.NewImages)}[{i}].{nameof(ImageCreatingRequestModel.Image)}", 
                $"image{i}{newImage.Format}");

            formData.Add(
                new StringContent(newImage.Index.ToString()),
                $"{nameof(UpdateModel.NewImages)}[{i++}].{nameof(ImageCreatingRequestModel.Index)}");
        }


        i = 0;

        foreach (var updatedImage in dto.UpdatedImages)
        {
            formData.Add(
                new StringContent(updatedImage.ImageIdentifier),
                $"{nameof(UpdateModel.UpdatedImages)}[{i}].{nameof(ImageUpdatingDto.ImageIdentifier)}");

            formData.Add(
                new StringContent(updatedImage.NewIndex.ToString()),
                $"{nameof(UpdateModel.UpdatedImages)}[{i++}].{nameof(ImageUpdatingDto.NewIndex)}");
        }


        i = 0;

        foreach (var deletedImage in dto.DeletedImages)
        {
            formData.Add(new StringContent(deletedImage), $"{nameof(UpdateModel.DeletedImages)}[{i++}]");
        }


        if (dto.NewOfferAgreement is not null)
        {
            var newOfferAgreement = dto.NewOfferAgreement;
            var newOfferContent = new StreamContent(newOfferAgreement.Stream);
            var extension = newOfferAgreement.Format.Extension;
            newOfferContent.Headers.ContentType = new MediaTypeHeaderValue(GetContentType(extension));
            formData.Add(newOfferContent, nameof(UpdateModel.NewOfferAgreement), Path.GetFileName($"new_offer_agreement{extension}"));
        }

        var response = await _httpClient.PutAsync(endpoint, formData);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }


    public async Task DeleteAsync(Guid id)
    {
        var endpoint = $"{_serverUrl}/announcements/{id}";

        var response = await _httpClient.DeleteAsync(endpoint);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }

    public async Task AddReservationAsync(ReservationCreatingDto reservationDto)
    {
        var endpoint = $"{_serverUrl}/announcements/reservations";

        var requestModel = new ReservationCreatingRequestModel(reservationDto);

        var json = JsonSerializer.Serialize(requestModel, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        });

        var content = new StringContent(json, Encoding.UTF8, "application/json");


        var response = await _httpClient.PostAsync(endpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            var errorContent = await response.Content.ReadAsStringAsync();

            throw new Exception(errorContent);
        }
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
