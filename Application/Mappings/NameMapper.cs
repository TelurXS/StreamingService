using Application.Features.Names;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class NameMapper : INameMapper
{
    public partial Name FromRequest(CreateName.Request request);

    public partial Name FromRequest(UpdateName.Request request);
}
