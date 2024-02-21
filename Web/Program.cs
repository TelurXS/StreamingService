using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web;
using Web.Interfaces;
using Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CookieAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(x => 
	x.GetRequiredService<CookieAuthenticationStateProvider>());

builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<ITitleService, TitleService>();
builder.Services.AddTransient<ITitlesListService, TitlesListService>();
builder.Services.AddTransient<ICommentsService, CommentService>();
builder.Services.AddTransient<IRateService, RateService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
