using API;
using API.Middlewares;
using Application.Extensions;
using Carter;
using Infrastructure.Extensions;
using API.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using API.Services;
using Domain.Interfaces.Services;

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
builder.Services.AddPolicies(builder.Configuration);
builder.Services.AddIdentity(builder.Configuration);

builder.Services.AddCarter();
builder.Services.AddAntiforgery();
builder.Services.AddSignalR();

builder.Services.AddSingleton<ExceptionHandler>();
builder.Services.AddSingleton<ClientRoutesService>();
builder.Services.AddSingleton<IPayPalService, PayPalService>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<IEmailSender<User>, EmailSender>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>,
		AdditionalUserClaimsPrincipalFactory>();

if (builder.Environment.IsProduction()) 
{
	builder.Services.AddHostedService<RolesSeeder>();
	builder.Services.AddHostedService<GenresSeeder>();
}

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
        options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
    })
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(3);
        options.Cookie.IsEssential = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.None;
        options.Cookie.SameSite = SameSiteMode.Unspecified;
    })
    .AddBearerToken(IdentityConstants.BearerScheme, options =>
    {

    });

builder.Services.AddAuthorization(o => o.DefaultPolicy =
    new AuthorizationPolicyBuilder(
        IdentityConstants.ApplicationScheme,
        IdentityConstants.BearerScheme)
    .RequireAuthenticatedUser()
    .Build()
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();
app.UseMiddleware<ExceptionHandler>();

app.MapCarter();

app.UseBlazorFrameworkFiles();
app.MapFallbackToFile("index.html");

app.Run();