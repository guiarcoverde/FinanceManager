using FinanceManager.Communication.Responses.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.UseCases.Expenses.GetAll;
public interface IGetAllExpensesUseCase
{
    Task<ResponseExpensesJson> Execute();
}
