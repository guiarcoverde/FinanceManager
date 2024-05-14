namespace FinanceManager.Exceptions.ExceptionsBase;

public class ErrorOnValidationException : FinanceManagerException
{
    public List<string> Errors { get; set; }
    public ErrorOnValidationException(List<string> errorsMessages)
    {
        Errors = errorsMessages;
    }
    
}
