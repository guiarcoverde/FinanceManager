using System.Net;

namespace FinanceManager.Exceptions.ExceptionsBase;

public class NotFoundException(string message) : FinanceManagerException(message)
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;
    public override List<string> GetErrors()
    {
        return [Message];
    }
}
