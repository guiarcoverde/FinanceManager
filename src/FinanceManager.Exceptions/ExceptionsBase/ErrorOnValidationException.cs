namespace FinanceManager.Exceptions.ExceptionsBase;

public class ErrorOnValidationException(List<string> errorsMessages) : FinanceManagerException(string.Empty)
{
    public List<string> Errors { get; set; } = errorsMessages;
}
