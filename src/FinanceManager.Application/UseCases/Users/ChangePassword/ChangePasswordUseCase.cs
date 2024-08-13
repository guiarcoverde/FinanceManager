using FinanceManager.Communication.Requests.Users;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Users;
using FinanceManager.Domain.Security.Cryptography;
using FinanceManager.Domain.Services.LoggedUser;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace FinanceManager.Application.UseCases.Users.ChangePassword;

public class ChangePasswordUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IPasswordEncryptor passwordEncryptor) : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUserUpdateOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordEncryptor _passwordEncryptor = passwordEncryptor;
    
    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.Get();
        
        Validate(request, loggedUser);
        
        var user = await _repository.GetById(loggedUser.Id);
        user.Password = _passwordEncryptor.Encrypt(request.NewPassword);
        
        _repository.Update(user);

        await _unitOfWork.Commit();

    }

    private void Validate(RequestChangePasswordJson request, User loggedUser)
    {
        var validator = new ChangePasswordValidator();
        var result = validator.Validate(request);

        var passwordMatch = _passwordEncryptor.Verify(request.Password, loggedUser.Password);
        
        if (passwordMatch is false)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessage.PASSWORD_DIFFERENT_CURRENT_PASSWORD));

        if (result.IsValid is true) return;
        var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errors);
    }
}