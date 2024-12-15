using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using LilaRent.Application.Dto;
using Stripe.Checkout;
using LilaRent.Requests.RequestModels;


namespace LilaRent.WebApi.Controllers;


[ApiController]
[Route("checkout")]
[ApiExplorerSettings(IgnoreApi = true)]
public class CheckoutController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private static string s_wasmClientURL = string.Empty;


    public CheckoutController(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    [HttpPost]
    public async Task<ActionResult> CheckoutOrder([FromForm] AnnouncementCreatingRequestModel announcement, [FromServices] IServiceProvider sp)
    {
        var apiKey = _configuration["Stripe:SecretKey"];

        //var referer = Request.Headers.Referer;
        //s_wasmClientURL = referer[0];

        //// Build the URL to which the customer will be redirected after paying.
        //var server = sp.GetRequiredService<IServer>();

        //var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

        //string? thisApiUrl = null;

        //if (serverAddressesFeature is not null)
        //{
        //    thisApiUrl = serverAddressesFeature.Addresses.FirstOrDefault();
        //}

        var thisApiUrl = "http://192.168.43.102:7002";

        var (sessionId, url) = await CheckOut(announcement, thisApiUrl, apiKey);
        var pubKey = _configuration["Stripe:PubKey"];

        var checkoutOrderResponse = new CheckoutAnnouncementResponse()
        {
            SessionId = sessionId,
            PubKey = pubKey,
            Url = url
        };

        return Ok(checkoutOrderResponse);
     
    }

    [HttpGet("success")]
    // Automatic query parameter handling from ASP.NET.
    // Example URL: https://localhost:7051/checkout/success?sessionId=si_123123123123
    public ActionResult CheckoutSuccess(string sessionId)
    {
        var sessionService = new SessionService();
        var session = sessionService.Get(sessionId);

        // Here you can save order and customer details to your database.
        var total = session.AmountTotal.Value;
        var customerEmail = session.CustomerDetails.Email;

        return Redirect(s_wasmClientURL + "success");
    }



    [NonAction]
    private async Task<(string, string)> CheckOut(AnnouncementCreatingRequestModel announcement, string thisApiUrl, string apiKey)
    {
        // Create a payment flow from the items in the cart.
        // Gets sent to Stripe API.
        var options = new SessionCreateOptions
        {
            // Stripe calls the URLs below when certain checkout events happen such as success and failure.
            SuccessUrl = $"https://www.google.by/?hl=ru", // Customer paid.
            CancelUrl = $"https://stackoverflow.com/questions/43650172/stripe-redirect-uri-will-not-work",  // Checkout cancelled.

            PaymentMethodTypes = new List<string> // Only card available in test mode?
            {
                "card"
            },
            LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (int)(announcement.Price.Value * 100) * 3, // Price is in USD cents.
                        Currency = "BYN",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = announcement.RentObjectName,
                            Description = announcement.Description,
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment" // One-time payment. Stripe supports recurring 'subscription' payments.
        };

        var client = new Stripe.StripeClient(apiKey);
        var service = new SessionService(client);

        try
        {
            var session = await service.CreateAsync(options);
            return (session.Id, session.Url);
        }
        catch (Exception ex)
        {
            ;
            throw;
        }
    }
}