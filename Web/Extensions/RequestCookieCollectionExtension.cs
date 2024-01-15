namespace Web.Extensions;

public static class RequestCookieCollectionExtension
{
    public static string ToCookieString(this IRequestCookieCollection cookies)
    {
        return string.Join("; ", cookies.Select(c => $"{c.Key}={c.Value}"));
    }
}
