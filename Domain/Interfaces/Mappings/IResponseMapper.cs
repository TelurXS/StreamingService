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

	TitleInfoResponse ToInfoResponse(Title value);

	UserResponse ToResponse(User value);

	ViewRecordResponse ToResponse(ViewRecord value);

	TitlesListResponse ToResponse(TitlesList value);

	NotificationResponse ToResponse(Notification value);
}
