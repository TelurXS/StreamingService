using Application.Features.LocalizedNames;
using Domain.Entities;

namespace Application.Interfaces.Mappings;

public interface ILocalizedNameMapper
{
    LocalizedName FromRequest(CreateName.Request request);

    LocalizedName FromRequest(UpdateName.Request request);
}
