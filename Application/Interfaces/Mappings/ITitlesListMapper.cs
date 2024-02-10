using Application.Features.Accounts;
using Application.Features.TitleLists;
using Application.Features.TitlesLists;
using Domain.Entities;

namespace Application.Interfaces.Mappings;

public interface ITitlesListMapper
{
	TitlesList FromRequest(CreateTitlesList.Request request);

	TitlesList FromRequest(UpdateTitlesList.Request request);
}
