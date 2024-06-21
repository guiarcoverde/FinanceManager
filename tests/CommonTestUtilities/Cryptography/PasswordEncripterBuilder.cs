using FinanceManager.Domain.Security.Cryptography;
using Moq;

namespace Common.TestUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    public static IPasswordEncryptor Build()
    {
        var mock = new Mock<IPasswordEncryptor>();

        mock.Setup(passwordEcripter => passwordEcripter.Encrypt(It.IsAny<string>())).Returns("o2341@$%!SDsafj");

        return mock.Object;
    }
}
