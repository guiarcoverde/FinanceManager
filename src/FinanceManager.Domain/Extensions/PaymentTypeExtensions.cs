using FinanceManager.Domain.Enums;
using FinanceManager.Domain.Reports;

namespace FinanceManager.Domain.Extensions;

public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => PaymentTypesMessages.CASH,
            PaymentType.CreditCard => PaymentTypesMessages.CREDIT_CARD,
            PaymentType.DebitCard => PaymentTypesMessages.DEBIT_CARD,
            PaymentType.ElectronicTransfer => PaymentTypesMessages.ELECTRONIC_TRANSFER,
            _ => string.Empty
        };
    }
}