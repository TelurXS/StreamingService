﻿using Application.Features.Names;
using Domain.Entities;

namespace Application.Interfaces.Mappings;

public interface INameMapper
{
    Name FromRequest(CreateName.Request request);

    Name FromRequest(UpdateName.Request request);
}
