﻿using Common.TestUtilities.Entities;
using FinanceManager.API;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using FinanceManager.Domain.Security.Cryptography;
using FinanceManager.Domain.Security.Tokens;
using FinanceManager.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Test.Resources;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public ExpenseIdentityManager ExpenseMemberTeam { get; private set; } = default!;
    public ExpenseIdentityManager ExpenseAdmin { get; private set; } = default!;
    public UserIdentityManager UserTeamMember { get; private set; } = default!;
    public UserIdentityManager UserAdmin { get; private set; } = default!;
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
                var accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                StartDatabase(dbContext, passwordEncryptor, accessTokenGenerator);


            });
    }
    
    private void StartDatabase(
        FinanceManagerDbContext dbContext, 
        IPasswordEncryptor passwordEncryptor, 
        IAccessTokenGenerator accessTokenGenerator)
    {
        var userTeamMember = AddUserTeamMember(dbContext, passwordEncryptor, accessTokenGenerator);
        var expenseTeamMember = AddExpenses(dbContext, userTeamMember, expenseId: 1, tagId: 1);
        ExpenseMemberTeam = new ExpenseIdentityManager(expenseTeamMember);
        
        var userAdmin = AddUserAdmin(dbContext, passwordEncryptor, accessTokenGenerator);
        var expenseAdmin = AddExpenses(dbContext, userAdmin, expenseId: 2, tagId: 2);
        ExpenseAdmin = new ExpenseIdentityManager(expenseAdmin);
        
        dbContext.SaveChanges();
    }

    private User AddUserTeamMember(FinanceManagerDbContext dbContext, 
        IPasswordEncryptor passwordEncryptor, 
        IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build();
        user.Id = 1;
        var password = user.Password;

        user.Password = passwordEncryptor.Encrypt(user.Password);

        dbContext.Users.Add(user);
        var token = accessTokenGenerator.Generate(user);
        
        UserTeamMember = new UserIdentityManager(user, password, token);

        return user;
    }
    
    private User AddUserAdmin(FinanceManagerDbContext dbContext, 
        IPasswordEncryptor passwordEncryptor, 
        IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build(Roles.Admin);
        user.Id = 2;
        var password = user.Password;

        user.Password = passwordEncryptor.Encrypt(user.Password);
        

        dbContext.Users.Add(user);
        var token = accessTokenGenerator.Generate(user);
        
        UserAdmin = new UserIdentityManager(user, password, token);

        return user;
    }


    private Expense AddExpenses(FinanceManagerDbContext dbContext, User user, long expenseId, long tagId)
    {
        var expense = ExpenseBuilder.Build(user);
        expense.Id = expenseId;

        foreach (var tag in expense.Tags)
        {
            tag.Id = tagId;
            tag.ExpenseId = expenseId;
        }
        
        dbContext.Expenses.Add(expense);

        return expense;
    }
    
    
}