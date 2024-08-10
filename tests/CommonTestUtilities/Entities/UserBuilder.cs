using Bogus;
using Common.TestUtilities.Cryptography;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;

namespace Common.TestUtilities.Entities;

public class UserBuilder
{
    public static User Build(string role = Roles.TeamMember)
    {
        var passwordEncriptor = new PasswordEncripterBuilder().Build();

        var user = new Faker<User>()
            .RuleFor(u => u.Id, _ = 1)
            .RuleFor(u => u.Name, faker => faker.Person.FirstName)
            .RuleFor(u => u.Email, (faker, user) => faker.Internet.Email(user.Name))
            .RuleFor(u => u.Password, (_, user) => passwordEncriptor.Encrypt(user.Password))
            .RuleFor(u => u.UserIdentifier, _ => Guid.NewGuid())
            .RuleFor(u => u.Role, _ => role);

        return user;
            
    }
}
