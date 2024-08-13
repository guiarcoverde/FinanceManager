using AutoMapper;
using FinanceManager.Communication.Responses.Users;
using FinanceManager.Domain.Services.LoggedUser;

namespace FinanceManager.Application.UseCases.Users.Profile.Get;

public class GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper) : IGetUserProfileUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IMapper _mapper = mapper;


    public async Task<ResponseUserProfileJson> Execute()
    {
        var user = await _loggedUser.Get();
        return _mapper.Map<ResponseUserProfileJson>(user);
    }
}