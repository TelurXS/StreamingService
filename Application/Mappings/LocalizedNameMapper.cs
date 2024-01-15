using Application.Features.LocalizedNames;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class LocalizedNameMapper : ILocalizedNameMapper
{
    public partial LocalizedName FromRequest(CreateName.Request request);

    public partial LocalizedName FromRequest(UpdateName.Request request);
}
