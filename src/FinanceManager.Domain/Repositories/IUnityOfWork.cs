namespace FinanceManager.Domain.Repositories;
public interface IUnityOfWork
{
    Task Commit();
}
