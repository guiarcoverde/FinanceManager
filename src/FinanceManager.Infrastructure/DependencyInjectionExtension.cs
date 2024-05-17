using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Expenses;
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
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnityOfWork, UnitOfWork>();
        services.AddScoped<IExpenseReadOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpenseWriteOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpenseUpdateOnlyRepository, ExpensesRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 37));


        services.AddDbContext<FinanceManagerDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
