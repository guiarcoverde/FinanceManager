﻿using FinanceManager.Domain.Repositories.Users;
using Moq;

namespace Common.TestUtilities.Repositories;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IUserReadOnlyRepository>();
    }
    public IUserReadOnlyRepository Build() => _repository.Object;
}
