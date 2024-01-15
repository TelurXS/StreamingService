using Application.Features.Images;
using Domain.Entities;

namespace Application.Interfaces.Mappings;

public interface IImageMapper
{
    Image FromRequest(CreateImage.Request request);

    Image FromRequest(UpdateImage.Request request);
}

