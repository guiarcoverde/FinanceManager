using Common.TestUtilities.Requests;
using FinanceManager.Application.UseCases;
using FinanceManager.Communication.Enums;
using FinanceManager.Exceptions;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;

public class ExpenseValidatorTest
{
    [Fact]
    public void Success()
        {
        //Arrange
        ExpenseValidator validator = new();
        var request = RequestExpenseJsonBuilder.Build();

        //Act

        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ErrorTitleEmpty()
    {
        ExpenseValidator validator = new();
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.TITLE_REQUIRED));
    }

    [Fact]
    public void ErrorDateFuture()
    {
        ExpenseValidator validator = new();
        var request = RequestExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.EXPENSES_CANNOT_BE_FUTURE));
    }
    [Fact]
    public void ErrorPaymentTypeInvalid()
    {
        ExpenseValidator validator = new();
        var request = RequestExpenseJsonBuilder.Build();
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
        ExpenseValidator validator = new();
        var request = RequestExpenseJsonBuilder.Build();
        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}
