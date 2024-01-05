using API.Middlewares;
using Application.Extensions;
using Carter;
using Infrastructure.Extensions;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(x => x.FullName!
        .Split('.')
        .Last()
        .Replace("+", ""));
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddIdentity(builder.Configuration);
builder.Services.ConfigureIdentity();

builder.Services.AddCarter();

builder.Services.AddSingleton<ExceptionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandler>();

app.MapIdentity();
app.MapCarter();

app.Run();