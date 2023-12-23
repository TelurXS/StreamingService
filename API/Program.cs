using Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
    Series = Enumerable.Range(1, 12).Select(x => new Series
    {
        Id = Guid.NewGuid(),
        Name = x.ToString(),
        Uri = $"https://images.com/image/hsegjhdrh{x}",
    }).ToList(),
    LocalizedNames = new List<LocalizedText>()
    {
        new LocalizedText()
        {
            Id = Guid.NewGuid(), 
            Language = "ua",
            Value = "Тестовий тайтл"
        },
        new LocalizedText()
        {
            Id = Guid.NewGuid(), 
            Language = "pl",
            Value = "Testowij kurwa taytl"
        },
    },
    LocalizedDescriptions = new List<LocalizedText>()
    {
        new LocalizedText()
        {
            Id = Guid.NewGuid(), 
            Language = "ua",
            Value = "Тестовий тайтл"
        },
        new LocalizedText()
        {
            Id = Guid.NewGuid(), 
            Language = "pl",
            Value = "Testowij kurwa taytl"
        },
    }
});

app.Run();