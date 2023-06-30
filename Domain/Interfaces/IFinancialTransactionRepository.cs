using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFinancialTransactionRepository
    {
        void AddFinancialTransactionAfterPaymentOrder(FinancialTransaction financial);
        void SaveChanges();
        List<FinancialTransaction> GetAllFinancialTransaction();
        decimal ReciveMoney();
        decimal ExportMoney();
    }
}
