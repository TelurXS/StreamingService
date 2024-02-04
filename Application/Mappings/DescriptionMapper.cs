using Application.Features.Descriptions;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class DescriptionMapper : IDescriptionsMapper
{
    public partial Description FromRequest(CreateDescription.Request request);

    public partial Description FromRequest(UpdateDescription.Request request);
}
