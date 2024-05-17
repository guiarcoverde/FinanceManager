using System.Net;

namespace FinanceManager.Exceptions.ExceptionsBase;

public class ErrorOnValidationException(List<string> errorsMessages) : FinanceManagerException(string.Empty)
{
    private readonly List<string> _errors = errorsMessages;
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
    public override List<string> GetErrors()
    {
        return _errors;
    }
}
