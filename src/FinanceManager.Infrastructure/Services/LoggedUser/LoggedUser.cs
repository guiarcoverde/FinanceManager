using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Security.Tokens;
using FinanceManager.Domain.Services.LoggedUser;
using FinanceManager.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinanceManager.Infrastructure.Services.LoggedUser;

public class LoggedUser(FinanceManagerDbContext dbContext, ITokenProvider tokenProvider) : ILoggedUser
{
    private readonly FinanceManagerDbContext _dbContext = dbContext;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
    }
}
