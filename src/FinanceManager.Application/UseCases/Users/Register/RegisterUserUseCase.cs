using AutoMapper;
using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Responses;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Users;
using FinanceManager.Domain.Security.Cryptography;
using FinanceManager.Domain.Security.Tokens;
using FinanceManager.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace FinanceManager.Application.UseCases.Users.Register;
public class RegisterUserUseCase(IMapper mapper, IPasswordEncryptor passwordEncryptor, IUserReadOnlyRepository userReadOnlyRepository, IUserWriteOnlyRepository userWriteOnlyRepository,IUnityOfWork unityOfWork, IAccessTokenGenerator tokenGenerator) : IRegisterUserUseCase
{

    private readonly IPasswordEncryptor _passwordEncryptor = passwordEncryptor;
    private readonly IMapper _mapper = mapper;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository = userWriteOnlyRepository;
    private readonly IAccessTokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncryptor.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);

        await _unityOfWork.Commit();

        return new ResponseRegisteredUserJson()
        {
            Name = user.Name,
            Token = _tokenGenerator.Generate(user)
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);
        var emailExists = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "Email already in use"));
        }

        if (result.IsValid is true) return;
        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }
}