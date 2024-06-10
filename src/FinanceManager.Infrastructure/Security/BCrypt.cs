using FinanceManager.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace FinanceManager.Infrastructure.Security;
internal class BCrypt : IPasswordEncryptor
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }
}
