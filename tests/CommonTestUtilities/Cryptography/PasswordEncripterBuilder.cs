using FinanceManager.Domain.Security.Cryptography;
using Moq;

namespace Common.TestUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    private readonly Mock<IPasswordEncryptor> _mock;

    public PasswordEncripterBuilder()
    {
        _mock = new Mock<IPasswordEncryptor>();

        _mock.Setup(passwordEcripter => passwordEcripter.Encrypt(It.IsAny<string>())).Returns("o2341@$%!SDsafj");
    }

    public PasswordEncripterBuilder Verify(string? password)
    {
        if (string.IsNullOrWhiteSpace(password) is false)
            _mock.Setup(passwordEncryptor => passwordEncryptor.Verify(password, It.IsAny<string>())).Returns(true);

        

        return this;
    }

    public IPasswordEncryptor Build() => _mock.Object;
}
