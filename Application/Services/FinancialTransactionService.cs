using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FinancialTransactionService : IFinancialTransactionService
    {
        private IFinancialTransactionRepository _financial;
        public FinancialTransactionService(IFinancialTransactionRepository financial)
        {
            _financial = financial;
        }

        public void AddFinancialTransactionAfterPaymentOrder(int orderid, int price, string BankReceipt, string BankTransferNumber)
        {
            FinancialTransaction financial = new FinancialTransaction()
            {
                OrderID = orderid,
                Price = price,
                BankReceipt = BankReceipt,
                BankTransferNumber = BankTransferNumber,
                CreateDate = DateTime.Now,
                FinancialTransactionTypeID = 1
            };

            _financial.AddFinancialTransactionAfterPaymentOrder(financial);
        }

        public void AddFinancialTransactionForReturendProduct(int orderid, int price, string BankReceipt, string BankTransferNumber)
        {
            FinancialTransaction financial = new FinancialTransaction()
            {
                OrderID = orderid,
                Price = price,
                BankReceipt = BankReceipt,
                BankTransferNumber = BankTransferNumber,
                CreateDate = DateTime.Now,
                FinancialTransactionTypeID = 2
            };

            _financial.AddFinancialTransactionAfterPaymentOrder(financial);
        }

        public decimal ExportMoney()
        {
            return _financial.ExportMoney();
        }

        public List<FinancialTransaction> GetAllFinancialTransaction()
        {
            return _financial.GetAllFinancialTransaction();
        }

        public decimal ReciveMoney()
        {
            return _financial.ReciveMoney();
        }
    }
}
