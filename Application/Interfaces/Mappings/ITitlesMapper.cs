﻿using Application.Features.Titles;
using Domain.Entities;

namespace Application.Interfaces.Mappings;

public interface ITitlesMapper
{
    Title FromRequest(CreateTitle.Request request);

    Title FromRequest(UpdateTitle.Request request);
}
