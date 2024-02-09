using Application.Features.TitleLists;
using Application.Features.TitlesLists;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class TitlesListMapper : ITitlesListMapper
{
	public partial TitlesList FromRequest(CreateTitlesList.Request request);

	public partial TitlesList FromRequest(UpdateTitlesList.Request request);
}
