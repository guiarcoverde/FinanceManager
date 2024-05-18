using FinanceManager.Communication.Requests.Incomes;
using FinanceManager.Exceptions;
using FluentValidation;

namespace FinanceManager.Application.UseCases;

public class IncomeValidator : AbstractValidator<RequestIncomeRegistrationJson>
{
    public IncomeValidator()
    {
        RuleFor(income => income.Title).NotEmpty().WithMessage(ResourceErrorMessage.TITLE_REQUIRED);
        RuleFor(income => income.Amount).GreaterThan(0).WithMessage(ResourceErrorMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(income => income.Source).IsInEnum().WithMessage(ResourceErrorMessage.INVALID_SOURCE);
    }
}