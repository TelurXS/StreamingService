using Application.Features.LocalizedDescriptions;
using Application.Features.LocalizedNames;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class LocalizedDescriptionMapper : ILocalizedDescriptionsMapper
{
    public partial LocalizedDescription FromRequest(CreateDescription.Request request);

    public partial LocalizedDescription FromRequest(UpdateDescription.Request request);
}
