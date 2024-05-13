using FinanceManager.Communication.Enums;
using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Responses;

namespace FinanceManager.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        return new ResponseRegisterExpenseJson()
        {
            Title = request.Title,
        };
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        bool titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);
        if (titleIsEmpty)
        {
            throw new ArgumentException("The title is obligatory.");
        }

        if (request.Amount <= 0)
        {
            throw new ArgumentException("The value must be greater than 0.");
        }

        int dateCompare = DateTime.Compare(request.Date, DateTime.UtcNow);
        if (dateCompare > 0)
        {
            throw new ArgumentException("Expenses cannot be for the future");
        }

        bool isPaymentTypeValid =Enum.IsDefined(typeof(PaymentType), request.PaymentType);
        if (!isPaymentTypeValid)
        {
            throw new ArgumentException("Payment type is not valid");
        }
    }
}
