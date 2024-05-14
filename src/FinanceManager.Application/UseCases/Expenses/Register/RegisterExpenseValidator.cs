using FinanceManager.Communication.Requests;
using FinanceManager.Exceptions;
using FluentValidation;

namespace FinanceManager.Application.UseCases.Expenses.Register;

public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceErrorMessage.TITLE_REQUIRED);
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage(ResourceErrorMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(expense => expense.Date).GreaterThan(DateTime.UtcNow).WithMessage(ResourceErrorMessage.EXPENSES_CANNOT_BE_FUTURE);
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage(ResourceErrorMessage.INVALID_PAYMENT_TYPE);
    }
}
