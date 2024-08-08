using Common.TestUtilities.Entities;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Security.Cryptography;
using FinanceManager.Domain.Security.Tokens;
using FinanceManager.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private User _user;
    private Expense _expense;
    private string _password;
    private string _token;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                services.AddDbContext<FinanceManagerDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanceManagerDbContext>();
                var passwordEncryptor = scope.ServiceProvider.GetRequiredService<IPasswordEncryptor>();

                StartDatabase(dbContext, passwordEncryptor);

                var tokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();
                _token = tokenGenerator.Generate(_user);
            });
    }
    public string GetName() => _user.Name;
    public string GetEmail() => _user.Email;
    public string GetPassword() => _password;
    public string GetToken() => _token;
    public long GetExpenseId() => _expense.Id;
    private void StartDatabase(FinanceManagerDbContext dbContext, IPasswordEncryptor passwordEncryptor)
    {
        AddUsers(dbContext, passwordEncryptor);
        AddExpenses(dbContext, _user);

        dbContext.SaveChanges();
    }

    private void AddUsers(FinanceManagerDbContext dbContext, IPasswordEncryptor passwordEncryptor)
    {
        _user = UserBuilder.Build();
        _password = _user.Password;

        _user.Password = passwordEncryptor.Encrypt(_user.Password);

        dbContext.Users.Add(_user);
    }

    private void AddExpenses(FinanceManagerDbContext dbContext, User user)
    {
        _expense = ExpenseBuilder.Build(user);
        dbContext.Expenses.Add(_expense);
    }
    
    
}