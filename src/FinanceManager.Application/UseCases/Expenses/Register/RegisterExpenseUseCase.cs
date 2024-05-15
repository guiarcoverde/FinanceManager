using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Responses;
using FinanceManager.Domain.Entities;
using FinanceManager.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace FinanceManager.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        var entity = new Expense()
        {
            Amount = request.Amount,
            Date = request.Date,
            Description = request.Description,
            Title = request.Title,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType,
        };

        return new ResponseRegisterExpenseJson();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        RegisterExpenseValidator validator = new();
        ValidationResult result = validator.Validate(request);

        if (!result.IsValid)
        {
            List<string> errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();   
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
