using FinanceManager.Application.AutoMapper;
using FinanceManager.Application.UseCases.Expenses.Delete;
using FinanceManager.Application.UseCases.Expenses.GetAll;
using FinanceManager.Application.UseCases.Expenses.GetById;
using FinanceManager.Application.UseCases.Expenses.Register;
using FinanceManager.Application.UseCases.Expenses.Reports.Excel;
using FinanceManager.Application.UseCases.Expenses.Reports.Pdf;
using FinanceManager.Application.UseCases.Expenses.Update;
using FinanceManager.Application.UseCases.Incomes.Delete;
using FinanceManager.Application.UseCases.Incomes.GetAll;
using FinanceManager.Application.UseCases.Incomes.GetById;
using FinanceManager.Application.UseCases.Incomes.Register;
using FinanceManager.Application.UseCases.Incomes.Update;
using FinanceManager.Application.UseCases.Login.DoLogin;
using FinanceManager.Application.UseCases.Users.ChangePassword;
using FinanceManager.Application.UseCases.Users.Profile.Get;
using FinanceManager.Application.UseCases.Users.Register;
using FinanceManager.Application.UseCases.Users.Update;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        /*
         * Expenses use cases
         */
        services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
        services.AddScoped<IGetAllExpensesUseCase, GetAllExpenses>();
        services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
        services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
        services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
        
        /*
         * Report Generation use cases
         */
        services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();
        services.AddScoped<IGenerateExpensesReportPdfUseCase, GenerateExpensesReportPdfUseCase>();
        
        /*
         * Incomes use cases
         */
        services.AddScoped<IRegisterIncomeUseCase, RegisterIncomeUseCase>();
        services.AddScoped<IGetAllIncomeUseCase, GetAllIncomesUseCase>();
        services.AddScoped<IGetIncomeByIdUseCase, GetByIdUseCase>();
        services.AddScoped<IDeleteIncomeUseCase, DeleteIncomeUseCase>();
        services.AddScoped<IUpdateIncomeUseCase, UpdateIncomeUseCase>();
        
        /*
         * User use cases
         */
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
    }


}
