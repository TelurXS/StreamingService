using API.Services;
using Carter;
using Domain.Interfaces.Services;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public sealed class PaymentEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/api/payment", CaptureOrderAsync)
			.RequireAuthorization();

		app.MapPost("/api/payment", CreateOrderAsync)
			.RequireAuthorization();
	}

	private static async Task<IResult> CaptureOrderAsync(
		[FromQuery] string orderId,
		[FromServices] IPayPalService payPalService
		)
	{
		var response = await payPalService.CaptureOrderAsync(orderId);

		var reference = response.PurchaseUnits.FirstOrDefault()?.ReferenceId;

		Console.WriteLine("REFERENCE: " + reference);

		// Put your logic to save the transaction here
		// You can use the "reference" variable as a transaction key

		return Results.Ok(response);
	}

	private static async Task<IResult> CreateOrderAsync(
		[FromServices] IPayPalService payPalService
		)
	{
		var price = "100.00";
		var currency = "USD";

		// "reference" is the transaction key
		var reference = "INV001";

		var response = await payPalService.CreateOrderAsync(price, currency, reference);

		return Results.Ok(response);
	}
}
