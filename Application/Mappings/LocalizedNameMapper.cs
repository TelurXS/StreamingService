using Application.Features.LocalizedNames;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class LocalizedNameMapper : INameMapper
{
    public partial Name FromRequest(CreateName.Request request);

    public partial Name FromRequest(UpdateName.Request request);
}
