using Application.Models;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using Infrastructure.Services;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Users;

public static class DeleteUserById
{
    public class Request : IRequest<DeleteResult>
    {
        [JsonIgnore]
        public Guid Id { get; set; } = default;
    }

    public class Handler : SyncRequestHandler<Request, DeleteResult>
    {
        public Handler(IUserService userService, IFileService fileService)
        {
            UserService = userService;
			FileService = fileService;
		}

        private IUserService UserService { get; }
		private IFileService FileService { get; }

		protected override DeleteResult Handle(Request request)
        {
			var userResult = UserService.FindById(request.Id);

			if (userResult.IsFound is false)
				return new NotFound();

			var user = userResult.AsFound;

			var currentImage = user.ProfileImage.Split("/").LastOrDefault();

			if (currentImage is not null)
				FileService.DeleteSeries(currentImage);

			return UserService.DeleteById(request.Id);
        }
    }
}
