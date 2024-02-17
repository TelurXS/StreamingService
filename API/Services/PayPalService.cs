using Domain.Interfaces.Services;
using Domain.Models.PayPal;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace API.Services;

public class PayPalService : IPayPalService
{
	private const string SECTION_NAME = "PayPal";

	private const string CLIENT_ID = "ClientId";
	private const string CLIENT_SECRET = "ClientSecret";
	private const string MODE = "Mode";

	private const string LIVE_MODE = "Live";

	private const string LIVE_URL = "https://api-m.paypal.com";
	private const string SANDBOX_URL = "https://api-m.sandbox.paypal.com";

	public PayPalService(IConfiguration configuration)
	{
		var section = configuration.GetSection(SECTION_NAME);

		ClientId = section.GetValue<string>(CLIENT_ID)!;
		ClientSecret = section.GetValue<string>(CLIENT_SECRET)!;
		Mode = section.GetValue<string>(MODE)!;

		BaseUrl = Mode.Equals(LIVE_MODE) ? LIVE_URL : SANDBOX_URL;
	}

	private string ClientId { get; }
	private string ClientSecret { get; }
	private string Mode { get; }

	private string BaseUrl { get; }

	private async Task<AuthResponse> AuthenticateAsync()
	{
		var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));

		var content = new List<KeyValuePair<string, string>>
		{
			new("grant_type", "client_credentials")
		};

		var request = new HttpRequestMessage
		{
			RequestUri = new Uri($"{BaseUrl}/v1/oauth2/token"),
			Method = HttpMethod.Post,
			Headers = { { "Authorization", $"Basic {auth}" } },
			Content = new FormUrlEncodedContent(content)
		};

		var client = new HttpClient();
		var response = await client.SendAsync(request);
		var json = await response.Content.ReadAsStringAsync();
		var result = JsonSerializer.Deserialize<AuthResponse>(json);

		return result!;
	}

	public async Task<CreateOrderResponse> CreateOrderAsync(string value, string currency, string reference)
	{

		var request = new CreateOrderRequest
		{
			Intent = "CAPTURE",
			PaymentSource = new PaymentSource
			{
				Paypal = new Paypal
				{
					Name = null,
					EmailAddress = null,
					AccountId = null,
					ExperienceContext = new ExperienceContext
					{
						ShippingPreference = "NO_SHIPPING",
					}
				}
			},
			PurchaseUnits = [
				new PurchaseUnit()
				{
					Shipping = null,
					Payments = null,
					ReferenceId = reference,
					Amount = new Amount
					{
						CurrencyCode = currency,
						Value = value
					},
				}
			],
		};

		var auth = await AuthenticateAsync();

		var client = new HttpClient();
		client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {auth.AccessToken}");

		var response = await client.PostAsJsonAsync($"{BaseUrl}/v2/checkout/orders", request);

		var json = await response.Content.ReadAsStringAsync();
		//Console.WriteLine(JsonSerializer.Serialize(request));
		//Console.WriteLine(json);
		var result = JsonSerializer.Deserialize<CreateOrderResponse>(json);

		return result!;
	}

	public async Task<CaptureOrderResponse> CaptureOrderAsync(string orderId)
	{
		var auth = await AuthenticateAsync();

		var client = new HttpClient();

		var content = new StringContent(string.Empty, Encoding.Default, MediaTypeNames.Application.Json);

		client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {auth.AccessToken}");

		var response = await client.PostAsync($"{BaseUrl}/v2/checkout/orders/{orderId}/capture", content);

		var json = await response.Content.ReadAsStringAsync();
		Console.WriteLine(json);
		var result = JsonSerializer.Deserialize<CaptureOrderResponse>(json);

		return result!;
	}
}
