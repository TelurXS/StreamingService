using Application.Features.Titles;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class TitleMapper : ITitleMapper
{
    public partial Title FromRequest(CreateTitle.Request request);

    public partial Title FromRequest(UpdateTitle.Request request);
}
