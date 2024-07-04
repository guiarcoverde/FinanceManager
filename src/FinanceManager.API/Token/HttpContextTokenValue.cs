using FinanceManager.Domain.Security.Tokens;

namespace FinanceManager.API.Token;

public class HttpContextTokenValue(IHttpContextAccessor httpContextAcessor) : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAcessor = httpContextAcessor;
    public string TokenOnRequest()
    {
        var authorization = _httpContextAcessor.HttpContext!.Request.Headers.Authorization.ToString();

        return authorization["Bearer ".Length..].Trim();
    }
}
