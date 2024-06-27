using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Responses;
using FinanceManager.Domain.Repositories.Users;
using FinanceManager.Domain.Security.Cryptography;
using FinanceManager.Domain.Security.Tokens;
using FinanceManager.Exceptions.ExceptionsBase;

namespace FinanceManager.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncryptor passwordEncryptor, IAccessTokenGenerator tokenGenerator) : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository = repository;
    private readonly IPasswordEncryptor _passwordEncryptor = passwordEncryptor;
    private readonly IAccessTokenGenerator _tokenGenerator = tokenGenerator;
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email) ?? throw new InvalidLoginException();
        var doesPasswordMatch = _passwordEncryptor.Verify(request.Password, user.Password);

        if (doesPasswordMatch is false)
            throw new InvalidLoginException();


        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _tokenGenerator.Generate(user)
        };
    }
}