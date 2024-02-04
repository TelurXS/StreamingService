using Domain.Entities;
using Domain.Models.Responses;

namespace Domain.Interfaces.Mappings;

public interface IResponseMapper
{
	CommentResponse ToResponse(Comment value);

	DescriptionResponse ToResponse(Description value);

	GenreResponse ToResponse(Genre value);

	ImageResponse ToResponse(Image value);

	NameResponse ToResponse(Name value);

	RateResponse ToResponse(Rate value);

	SeriesResponse ToResponse(Series value);

	SubscriptionResponse ToResponse(Subscription value);

	TitleResponse ToResponse(Title value);

	UserResponse ToResponse(User value);
}
