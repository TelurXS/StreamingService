using Application.Features.Images;
using Application.Interfaces.Mappings;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class ImageMapper : IImageMapper
{
    public partial Image FromRequest(CreateImage.Request request);

    public partial Image FromRequest(UpdateImage.Request request);
}
