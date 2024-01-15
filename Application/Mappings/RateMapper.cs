using Application.Features.Rates;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class RateMapper : IRateMapper
{
    public partial Rate FromRequest(CreateRate.Request request);

    public partial Rate FromRequest(UpdateRate.Request request);
}
