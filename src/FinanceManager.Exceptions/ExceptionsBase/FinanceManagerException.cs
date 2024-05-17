namespace FinanceManager.Exceptions.ExceptionsBase;

public abstract class FinanceManagerException(string message) : SystemException(message)
{
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}

