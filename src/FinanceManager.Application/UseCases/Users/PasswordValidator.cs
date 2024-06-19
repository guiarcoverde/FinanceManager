using System.Text.RegularExpressions;
using FinanceManager.Exceptions;
using FluentValidation;
using FluentValidation.Validators;

namespace FinanceManager.Application.UseCases.Users;
public class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ErrorMessageKey = "ErrorMessage";
    public override string Name => "PasswordValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ErrorMessageKey}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessage.PASSWORD_INVALID);
            return false;
        }

        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessage.PASSWORD_INVALID);
            return false;
        }

        if (Regex.IsMatch(password, @"[A-Z]+") is false)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessage.PASSWORD_INVALID);
            return false;
        }

        if (Regex.IsMatch(password, @"[a-z]+") is false)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessage.PASSWORD_INVALID);
            return false;
        }

        if (Regex.IsMatch(password, @"[0-9]+") is false)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessage.PASSWORD_INVALID);
            return false;
        }

        if (Regex.IsMatch(password, @"[\!\?\*\.]+") is false)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessage.PASSWORD_INVALID);
            return false;
        }

        return true;
    }
}