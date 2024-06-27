
using System.Net;

namespace FinanceManager.Exceptions.ExceptionsBase;

public class InvalidLoginException : FinanceManagerException
{
    public InvalidLoginException() : base(ResourceErrorMessage.INVALID_USERNAME_AND_PASSWORD)
    { }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
