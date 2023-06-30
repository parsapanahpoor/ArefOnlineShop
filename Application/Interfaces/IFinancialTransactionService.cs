using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFinancialTransactionService
    {
        void AddFinancialTransactionAfterPaymentOrder(int orderid, int price, string BankReceipt, string BankTransferNumber);
        void AddFinancialTransactionForReturendProduct(int orderid, int price, string BankReceipt, string BankTransferNumber);
        List<FinancialTransaction> GetAllFinancialTransaction();
        decimal ReciveMoney();
        decimal ExportMoney();

    }
}
