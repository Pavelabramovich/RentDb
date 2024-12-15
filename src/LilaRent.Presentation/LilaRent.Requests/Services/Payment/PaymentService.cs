using LilaRent.Application.Dto;
using LilaRent.Domain.Fields;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;
using CreateModel = LilaRent.Requests.RequestModels.AnnouncementCreatingRequestModel;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.StaticFiles;
using System.Text.Json.Serialization;


namespace LilaRent.Requests.Services.Payment;


public class PaymentService
{
    private readonly HttpClient _httpClient;
    private readonly string _serverUrl;


    public PaymentService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        _serverUrl = configuration["UriData:ApiUri"]
            ?? throw new ArgumentException("There is no UriData:ApiUri in configuration");
    }



    public async Task<string> GetPaymentUrlAsync(AnnouncementCreatingDto dto)
    {
        string endpoint = $"{_serverUrl}/checkout";

        //  var json = JsonSerializer.Serialize(dto);
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

        response.EnsureSuccessStatusCode();

      //  var responseBody = await response.Content.ReadAsStringAsync();


        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        // var json = await response.Content.ReadAsStringAsync();

        var checkoutResponse = await response.Content.ReadFromJsonAsync<CheckoutAnnouncementResponse>(options)
            ?? throw new InvalidCastException("Can not cast responce to tokens");



        // var checkoutResponse = JsonSerializer.Deserialize<CheckoutAnnouncementResponse>(responseBody);

        //  string checkoutUrl = $"https://checkout.stripe.com/pay/{checkoutResponse.SessionId}";

        return checkoutResponse.Url;
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
