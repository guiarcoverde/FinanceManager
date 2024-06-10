using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Expenses;
using FinanceManager.Domain.Repositories.Incomes;
using FinanceManager.Domain.Repositories.Users;
using FinanceManager.Domain.Security.Cryptography;
using FinanceManager.Infrastructure.DataAccess;
using FinanceManager.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);

        services.AddScoped<IPasswordEncryptor, Security.BCrypt>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnityOfWork, UnitOfWork>();
        
        services.AddScoped<IExpenseReadOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpenseWriteOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpenseUpdateOnlyRepository, ExpensesRepository>();
        
        services.AddScoped<IIncomeWriteOnlyRepository, IncomesRepository>();
        services.AddScoped<IIncomeReadOnlyRepository, IncomesRepository>();
        services.AddScoped<IIncomeUpdateOnlyRepository, IncomesRepository>();

        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var mySqlConnectionString = configuration.GetConnectionString("MySqlConnection");
        var version = new Version(8, 0, 37);
        var serverVersion = new MySqlServerVersion(version);
        
        services.AddDbContext<FinanceManagerDbContext>(config => config.UseMySql(mySqlConnectionString, serverVersion));

    }
    
}
