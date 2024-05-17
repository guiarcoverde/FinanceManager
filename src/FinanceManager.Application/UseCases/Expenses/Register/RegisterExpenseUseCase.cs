using AutoMapper;
using FinanceManager.Communication.Requests;
using FinanceManager.Communication.Responses.Register;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Expenses;
using FinanceManager.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace FinanceManager.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase(IExpenseWriteOnlyRepository repository, IUnityOfWork unityOfWork, IMapper mapper) : IRegisterExpenseUseCase
{

    private readonly IExpenseWriteOnlyRepository _repository = repository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request)
    {
        Validate(request);

        var entity = _mapper.Map<Expense>(request);

        await _repository.Add(entity);

        await _unityOfWork.Commit();

        return _mapper.Map<ResponseRegisterExpenseJson>(entity);
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
