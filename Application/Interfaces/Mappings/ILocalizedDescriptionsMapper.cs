using Application.Features.LocalizedDescriptions;
using Domain.Entities;

namespace Application.Interfaces.Mappings;

public interface ILocalizedDescriptionsMapper
{
    LocalizedDescription FromRequest(CreateDescription.Request request);

    LocalizedDescription FromRequest(UpdateDescription.Request request);
}
