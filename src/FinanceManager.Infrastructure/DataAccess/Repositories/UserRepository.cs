using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.DataAccess.Repositories;

internal class UserRepository(FinanceManagerDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly FinanceManagerDbContext _dbContext = dbContext;

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
       return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));
    }

    public async Task<User?> GetUserByEmail(string email) =>
        await _dbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email.Equals(email));

    public async Task<User> GetById(long id)
        => await _dbContext.Users.FirstAsync(user => user.Id == id);
    
    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }
}