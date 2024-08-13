using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Expenses;
using FinanceManager.Domain.Repositories.Incomes;
using FinanceManager.Domain.Repositories.Users;
using FinanceManager.Domain.Security.Cryptography;
using FinanceManager.Domain.Security.Tokens;
using FinanceManager.Domain.Services.LoggedUser;
using FinanceManager.Infrastructure.DataAccess;
using FinanceManager.Infrastructure.DataAccess.Repositories;
using FinanceManager.Infrastructure.Extensions;
using FinanceManager.Infrastructure.Security.Tokens;
using FinanceManager.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncryptor, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>(); 
        
        AddToken(services, configuration);
        AddRepositories(services);

        if (configuration.IsTestEnvironment() is false)
            AddDbContext(services, configuration);

    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IExpenseReadOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpenseWriteOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpenseUpdateOnlyRepository, ExpensesRepository>();
        
        services.AddScoped<IIncomeWriteOnlyRepository, IncomesRepository>();
        services.AddScoped<IIncomeReadOnlyRepository, IncomesRepository>();
        services.AddScoped<IIncomeUpdateOnlyRepository, IncomesRepository>();

        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var mySqlConnectionString = configuration.GetConnectionString("MySqlConnection");
        var serverVersion = ServerVersion.AutoDetect(mySqlConnectionString);
        
        services.AddDbContext<FinanceManagerDbContext>(config => config.UseMySql(mySqlConnectionString, serverVersion));

    }
    private static void AddToken(this IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");
        services.AddScoped<IAccessTokenGenerator>(opt => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }

}
