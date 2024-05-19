using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.DataAccess.Repositories;

internal class UsersRepository(FinanceManagerDbContext dbContext) : IUserWriteOnlyRepository
{

    private readonly FinanceManagerDbContext _dbContext = dbContext;
    
    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<User> GetByUserName(string username)
    {
        var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

        if (result is null)
        {
            throw new Exception("User not found");
        }
        
        return result;
    }
}