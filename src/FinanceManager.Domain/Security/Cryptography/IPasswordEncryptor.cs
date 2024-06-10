namespace FinanceManager.Domain.Security.Cryptography;

public interface IPasswordEncryptor
{
    string Encrypt (string password);
}