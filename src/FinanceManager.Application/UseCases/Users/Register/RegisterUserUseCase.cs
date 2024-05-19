using AutoMapper;
using FinanceManager.Communication.Requests.Users;
using FinanceManager.Communication.Responses.Users;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Users;
using FinanceManager.Exceptions.ExceptionsBase;

namespace FinanceManager.Application.UseCases.Users.Register;

public class RegisterUserUseCase(IUserWriteOnlyRepository repository, IUnityOfWork unityOfWork, IMapper mapper) : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _repository = repository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IMapper _mapper = mapper;
    
    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        Validator(request);

        var entity = _mapper.Map<User>(request);
        
        await _repository.Add(entity);
        await _unityOfWork.Commit();

        return _mapper.Map<ResponseRegisterUserJson>(entity);
    }

    private void Validator(RequestRegisterUserJson request)
    {
        UserValidator validator = new();
        
        var result = validator.Validate(request);

        if (result.IsValid is false)
        {
            List<string> errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}