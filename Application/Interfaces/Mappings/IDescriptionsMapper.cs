﻿using Application.Features.LocalizedDescriptions;
using Domain.Entities;

namespace Application.Interfaces.Mappings;

public interface IDescriptionsMapper
{
    Description FromRequest(CreateDescription.Request request);

    Description FromRequest(UpdateDescription.Request request);
}
