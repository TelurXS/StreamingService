using API.Hubs;
using Carter;
using Domain.Models;

namespace API.Endpoints;

public class HubEndpoints : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapHub<SessionHub>(ApiRoutes.Hubs.Sessions);
	}
}
