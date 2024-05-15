using FinanceManager.Application.UseCases.Expenses.Register;
using CommonTestUtilities.Requests;
using FluentAssertions;
using FinanceManager.Exceptions;
using FinanceManager.Communication.Enums;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTest
{
    [Fact]
    public void Success()
        {
        //Arrange
        RegisterExpenseValidator validator = new();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        //Act

        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ErrorTitleEmpty()
    {
        RegisterExpenseValidator validator = new();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.TITLE_REQUIRED));
    }

    [Fact]
    public void ErrorDateFuture()
    {
        RegisterExpenseValidator validator = new();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSES_CANNOT_BE_FUTURE));
    }
    [Fact]
    public void ErrorPaymentTypeInvalid()
    {
        RegisterExpenseValidator validator = new();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.INVALID_PAYMENT_TYPE));
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-7)]
    public void ErrorAmountGreaterThanZero(decimal amount)
    {
        RegisterExpenseValidator validator = new();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}
