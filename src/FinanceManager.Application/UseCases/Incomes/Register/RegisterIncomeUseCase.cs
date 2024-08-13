using AutoMapper;
using FinanceManager.Communication.Requests.Incomes;
using FinanceManager.Communication.Responses.Incomes.Register;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Repositories.Incomes;
using FinanceManager.Exceptions.ExceptionsBase;

namespace FinanceManager.Application.UseCases.Incomes.Register;


public class RegisterIncomeUseCase(IIncomeWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IRegisterIncomeUseCase
{
    private readonly IIncomeWriteOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    
    
    public async Task<ResponseRegisterIncomeJson> Execute(RequestIncomeRegistrationJson request)
    {
        Validate(request);
        
        var entity = _mapper.Map<Income>(request);
        
        await _repository.Add(entity);
        
        await _unitOfWork.Commit();
        
        return _mapper.Map<ResponseRegisterIncomeJson>(entity);

    }


    private void Validate(RequestIncomeRegistrationJson request)
    {
        IncomeValidator validator = new();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            List<string> errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();   
            throw new ErrorOnValidationException(errorMessages);
        }
    }


}