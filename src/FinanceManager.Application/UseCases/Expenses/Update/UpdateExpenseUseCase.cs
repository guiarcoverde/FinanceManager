﻿using AutoMapper;
using FinanceManager.Communication.Requests.Expenses;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Expenses;
using FinanceManager.Domain.Services.LoggedUser;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;

namespace FinanceManager.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase(IMapper mapper, IUnityOfWork unityOfWork, IExpenseUpdateOnlyRepository repository, ILoggedUser loggedUser) : IUpdateExpenseUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IExpenseUpdateOnlyRepository _repository = repository;
    private readonly ILoggedUser _loggedUser = loggedUser;


    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var expense = await _repository.GetById(loggedUser, id);

        if (expense is null)
        {
            throw new NotFoundException(ResourceErrorMessage.EXPENSE_NOT_FOUND);
        }

        _mapper.Map(request, expense);
        
        _repository.Update(expense);
        await _unityOfWork.Commit();
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        var result = validator.Validate(request);

        if (result.IsValid) return;
        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}