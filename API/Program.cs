using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/title", () => new Title
{
    Id = Guid.NewGuid(),
    Name = "Test Title",
    Description = "Lorem ipsum dolor ame amenus po septum go dolor",
    Slug = "test-title-season-1",
    Image = new Image
    {
        Id = Guid.NewGuid(),
        Uri = "https://images.com/image/fawfawfafgawgp"
    },
    Screenshots = new List<Image>()
    {
        new Image
        {
            Id = Guid.NewGuid(),
            Uri = "https://images.com/image/hsegjhdrh"
        },
        new Image
        {
            Id = Guid.NewGuid(),
            Uri = "https://images.com/image/fwagawgsghe"
        },
    },
    Series = Enumerable.Range(1,
            12)
        .Select(x => new Series
        {
            Id = Guid.NewGuid(),
            Name = x.ToString(),
            Uri = $"https://images.com/image/hsegjhdrh{x}",
            Title = null,
        })
        .ToList(),
    LocalizedNames = new List<LocalizedName>()
    {
        new LocalizedName()
        {
            Id = Guid.NewGuid(),
            Language = "ua",
            Value = "Тестовий тайтл",
            Title = null,
        },
        new LocalizedName()
        {
            Id = Guid.NewGuid(),
            Language = "pl",
            Value = "Testowij kurwa taytl",
            Title = null,
        },
    },
    LocalizedDescriptions = new List<LocalizedDescription>()
    {
        new LocalizedDescription()
        {
            Id = Guid.NewGuid(),
            Language = "ua",
            Value = "Тестовий тайтл",
            Title = null,
        },
        new LocalizedDescription()
        {
            Id = Guid.NewGuid(),
            Language = "pl",
            Value = "Testowij kurwa taytl",
            Title = null,
        },
    },
    ReleaseDate = DateTime.Now,
    Genres = null,
    Rates = null
});

app.Run();