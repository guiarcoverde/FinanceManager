﻿namespace FinanceManager.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
