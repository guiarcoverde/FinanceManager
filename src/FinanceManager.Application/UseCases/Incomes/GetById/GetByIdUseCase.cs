using AutoMapper;
using FinanceManager.Communication.Responses.Incomes.GetIncomeById;
using FinanceManager.Domain.Repositories.Incomes;
using FinanceManager.Exceptions;
using FinanceManager.Exceptions.ExceptionsBase;

namespace FinanceManager.Application.UseCases.Incomes.GetById;

public class GetByIdUseCase(IIncomeReadOnlyRepository repository, IMapper mapper) : IGetIncomeByIdUseCase
{
    private readonly IIncomeReadOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<ResponseIncomeJson> Execute(long id)
    {
        var result = await _repository.GetById(id) ??
                     throw new NotFoundException(ResourceErrorMessage.INCOME_NOT_FOUND);

        return _mapper.Map<ResponseIncomeJson>(result);

    }
}