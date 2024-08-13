using FinanceManager.Communication.Requests.Users;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Users;
using FinanceManager.Domain.Services.LoggedUser;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace FinanceManager.Application.UseCases.Users.Update;

public class UpdateUserUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository repository, IUserReadOnlyRepository readOnlyRepository, IUnitOfWork unitOfWork) : IUpdateUserUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUserUpdateOnlyRepository _repository = repository;
    private readonly IUserReadOnlyRepository _readOnlyRepository = readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
    public async Task Execute(RequestUpdateUserJson request)
    {
        var loggedUser = await _loggedUser.Get();
        await Validate(request, loggedUser.Email);

        var user = await _repository.GetById(loggedUser.Id);

        user.Name = request.Name;
        user.Email = request.Email;
        
        _repository.Update(user);

        await _unitOfWork.Commit();

    }

    private async Task Validate(RequestUpdateUserJson request, string currentEmail)
    {
        var validator = new UpdateUserValidator();

        var result = validator.Validate(request);

        if (currentEmail.Equals(request.Email) == false)
        {
            var userExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if(userExist)
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessage.EMAIL_ALREADY_REGISTERED));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}