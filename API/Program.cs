using API;
using API.Middlewares;
using Application.Extensions;
using Carter;
using Infrastructure.Extensions;
using API.Extensions;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

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

builder.Services.AddSingleton<ExceptionHandler>();

builder.Services
	.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
		options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
		options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
	})
	.AddCookie(IdentityConstants.ApplicationScheme, options =>
	{
		options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.Cookie.IsEssential = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None;
    })
	.AddBearerToken(IdentityConstants.BearerScheme, options => 
	{
		
	});

var multiSchemePolicy = new AuthorizationPolicyBuilder(
	IdentityConstants.ApplicationScheme,
	IdentityConstants.BearerScheme)
  .RequireAuthenticatedUser()
  .Build();

builder.Services.AddAuthorization(o => o.DefaultPolicy = multiSchemePolicy);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseCors();
app.UseMiddleware<ExceptionHandler>();

app.MapCarter();

app.Run();