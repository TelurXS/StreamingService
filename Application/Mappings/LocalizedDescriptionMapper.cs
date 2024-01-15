using Application.Features.LocalizedDescriptions;
using Application.Features.LocalizedNames;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class LocalizedDescriptionMapper : IDescriptionsMapper
{
    public partial Description FromRequest(CreateDescription.Request request);

    public partial Description FromRequest(UpdateDescription.Request request);
}
