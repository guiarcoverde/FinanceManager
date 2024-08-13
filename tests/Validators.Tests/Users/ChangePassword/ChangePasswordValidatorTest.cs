using Common.TestUtilities.Requests;
using FinanceManager.Application.UseCases.Users.ChangePassword;
using FinanceManager.Communication.Requests.Users;
using FinanceManager.Exceptions;
using FluentAssertions;

namespace Validators.Tests.Users.ChangePassword;

public class ChangePasswordValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new ChangePasswordValidator();
        var request = RequestChangePasswordJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void EmptyNewPassword(string newPassword)
    {
        var validator = new ChangePasswordValidator();

        var request = RequestChangePasswordJsonBuilder.Build();
        request.NewPassword = newPassword;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .ContainSingle()
            .And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.PASSWORD_INVALID));    
    }
}