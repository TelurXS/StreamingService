using Application.Models;
using Domain.Interfaces.Services;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.TitlesLists;

public class DeleteTitlesListById
{
	public class Request : IRequest<DeleteResult>
	{
		[JsonIgnore]
		public Guid Id { get; set; } = default;
	}

	public class Handler : SyncRequestHandler<Request, DeleteResult>
	{
		public Handler(ITitlesListService titlesListService)
		{
			TitlesListService = titlesListService;
		}

		private ITitlesListService TitlesListService { get; }

		protected override DeleteResult Handle(Request request)
		{
			return TitlesListService.DeleteById(request.Id);
		}
	}
}
