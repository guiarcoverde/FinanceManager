using AutoMapper;
using FinanceManager.Communication.Requests.Incomes;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Incomes;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;

namespace FinanceManager.Application.UseCases.Incomes.Update;

public class UpdateIncomeUseCase(IIncomeUpdateOnlyRepository repository, IUnityOfWork unityOfWork, IMapper mapper) : IUpdateIncomeUseCase
{
    private readonly IIncomeUpdateOnlyRepository _repository = repository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IMapper _mapper = mapper;
    
    public async Task Execute(long id, RequestIncomeUpdateJson request)
    {
        Validate(request);
        
        var income = await _repository.GetById(id);

        if (income is null)
        {
            throw new NotFoundException(ResourceErrorMessage.INCOME_NOT_FOUND);
        }

        _mapper.Map(request, income);
        _repository.Update(income);
        await _unityOfWork.Commit();
    }
    
    private void Validate(RequestIncomeUpdateJson request)
    {
        var validator = new IncomeUpdateValidator();
        var result = validator.Validate(request);

        if (result.IsValid) return;
        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
}