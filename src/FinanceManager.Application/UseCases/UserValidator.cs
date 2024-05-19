using FinanceManager.Communication.Requests.Users;
using FinanceManager.Exceptions;
using FluentValidation;

namespace FinanceManager.Application.UseCases;

public class UserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public UserValidator()
    {
        RuleFor(user => user.Username).NotEmpty().WithMessage(ResourceErrorMessage.LOGIN_REQUIRED);
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceErrorMessage.EMAIL_REQUIRED);
        RuleFor(user => user.Password).NotEmpty().WithMessage(ResourceErrorMessage.PASSWORD_REQUIRED);
    }
}