﻿using Application.Models;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Users;

public static class RemoveProfileImageToUser
{
	public class Request : IRequest<UpdateResult<Success>>
	{
		[JsonIgnore]
		public Guid Id { get; set; }
	}

	public class Handler : SyncRequestHandler<Request, UpdateResult<Success>>
	{
		public Handler(IUserService userService, IFileService fileService)
		{
			UserService = userService;
			FileService = fileService;
		}

		public IUserService UserService { get; }
		public IFileService FileService { get; }

		protected override UpdateResult<Success> Handle(Request request)
		{
			var userResult = UserService.FindById(request.Id);

			if (userResult.IsFound is false)
				return new NotFound();

			var user = userResult.AsFound;

			var currentImage = user.ProfileImage.Split("/").LastOrDefault();

			if (currentImage is not null)
				FileService.DeleteUserImage(currentImage);

			user.ProfileImage = string.Empty;

			var result = UserService.Update(request.Id, user);

			if (result.IsUpdated)
				return new Success();

			return new Failed();
		}
	}
	
}
