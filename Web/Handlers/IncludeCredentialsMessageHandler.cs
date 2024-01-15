
using Microsoft.Net.Http.Headers;
using System.Net;
using Web.Extensions;

namespace Web.Handlers;

public sealed class IncludeCredentialsMessageHandler : DelegatingHandler
{
    public IncludeCredentialsMessageHandler(IHttpContextAccessor contextAccessor)
    {
        ContextAccessor = contextAccessor;
    }

    private IHttpContextAccessor ContextAccessor { get; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var context = ContextAccessor.HttpContext;

        if (context is null)
            return base.SendAsync(request, cancellationToken);

        var cookies = context.Request.Cookies;

        if (cookies.Count == 0)
            return base.SendAsync(request, cancellationToken);

        var cookieString = cookies.ToCookieString();

        request.Headers.Add(HeaderNames.Cookie, cookieString);

        return base.SendAsync(request, cancellationToken);
    }
}
