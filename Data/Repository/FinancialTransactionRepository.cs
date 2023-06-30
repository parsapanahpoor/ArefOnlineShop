using Data.Context;
using Domain.Interfaces;
using Domain.Models.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private ParsaWorkShopContext _context;
        public FinancialTransactionRepository(ParsaWorkShopContext context)
        {
            _context = context;
        }

        public void AddFinancialTransactionAfterPaymentOrder(FinancialTransaction financial)
        {
            _context.FinancialTransactions.Add(financial);
            SaveChanges();
        }

        public decimal ExportMoney()
        {
            return _context.FinancialTransactions.Where(p => p.FinancialTransactionTypeID == 2)
                                .Sum(p => p.Price);
        }

        public List<FinancialTransaction> GetAllFinancialTransaction()
        {
            return _context.FinancialTransactions.Include(p => p.Order)
                                             .ThenInclude(p => p.User).ToList();                                
        }

        public decimal ReciveMoney()
        {
            return _context.FinancialTransactions.Where(p => p.FinancialTransactionTypeID == 1)
                                            .Sum(p => p.Price);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
