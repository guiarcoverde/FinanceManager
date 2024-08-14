﻿using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Requests.Expenses;
using FinanceManager.Exceptions;
using FluentValidation;

namespace FinanceManager.Application.UseCases;

public class ExpenseValidator : AbstractValidator<RequestExpenseJson>
{
    public ExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceErrorMessage.TITLE_REQUIRED);
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage(ResourceErrorMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessage.EXPENSES_CANNOT_BE_FUTURE);
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage(ResourceErrorMessage.INVALID_PAYMENT_TYPE);
        RuleFor(expense => expense.Tags)
            .ForEach(rule =>
            {
                rule.IsInEnum().WithMessage(ResourceErrorMessage.TAG_TYPE_NOT_SUPPORTED);
            });
    }
}
