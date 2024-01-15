using Application.Features.Rates;
using Domain.Entities;

namespace Application.Interfaces.Mappings;

public interface IRateMapper
{
    Rate FromRequest(CreateRate.Request request);

    Rate FromRequest(UpdateRate.Request request);
}
