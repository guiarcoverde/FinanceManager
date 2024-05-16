namespace FinanceManager.Exceptions.ExceptionsBase;

public class NotFoundException(string message) : FinanceManagerException(message)
{
}
