using Domain.Entities;
using Domain.Interfaces.Mappings;
using Domain.Models.Responses;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class ResponseMapper : IResponseMapper
{
	public partial CommentResponse ToResponse(Comment value);

	public partial DescriptionResponse ToResponse(Description value);
		   
	public partial GenreResponse ToResponse(Genre value);
		   
	public partial ImageResponse ToResponse(Image value);
		   
	public partial NameResponse ToResponse(Name value);
		   
	public partial RateResponse ToResponse(Rate value);
		   
	public partial SeriesResponse ToResponse(Series value);
		   
	public partial SubscriptionResponse ToResponse(Subscription value);
		   
	public partial TitleResponse ToResponse(Title value);
		   
	public partial UserResponse ToResponse(User value);
}
