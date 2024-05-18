using AutoMapper;
using FinanceManager.Communication.Responses.Incomes;
using FinanceManager.Communication.Responses.Incomes.GetAll;
using FinanceManager.Domain.Repositories.Incomes;

namespace FinanceManager.Application.UseCases.Incomes.GetAll;

public class GetAllIncomesUseCase(IIncomeReadOnlyRepository repository, IMapper mapper) : IGetAllIncomeUseCase
{
    private readonly IIncomeReadOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<ResponseIncomesJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseIncomesJson()
        {
            Incomes = _mapper.Map<List<ResponseShortIncome>>(result)
        };
    }
}