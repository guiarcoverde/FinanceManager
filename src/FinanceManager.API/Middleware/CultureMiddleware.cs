using System.Globalization;

namespace FinanceManager.API.Middleware;

public class CultureMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        List<CultureInfo> supportedLanguages = [.. CultureInfo.GetCultures(CultureTypes.AllCultures)];
        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultureInfo = new CultureInfo("en");

        if (string.IsNullOrWhiteSpace(requestedCulture) == false
            && supportedLanguages.Exists(x => x.Name.Equals(requestedCulture))
            )
        {
            cultureInfo = new CultureInfo(requestedCulture);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}
