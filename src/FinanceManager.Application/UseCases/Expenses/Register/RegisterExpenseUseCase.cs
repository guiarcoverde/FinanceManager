using AutoMapper;
using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Requests.Expenses;
using FinanceManager.Communication.Responses.Expenses.Register;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Expenses;
using FinanceManager.Domain.Services.LoggedUser;
using FinanceManager.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace FinanceManager.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase(IExpenseWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper, ILoggedUser loggedUser) : IRegisterExpenseUseCase
{

    private readonly IExpenseWriteOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var expense = _mapper.Map<Expense>(request);
        expense.UserId = loggedUser.Id;

        await _repository.Add(expense);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterExpenseJson>(expense);
    }

    private void Validate(RequestExpenseJson request)
    {
        ExpenseValidator validator = new();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            List<string> errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();   
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
