using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class FinancialTransactionType
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.None)]
        public int FinancialTransactionTypeID { get; set; }

        public string Title { get; set; }

        #region Navigations

        public List<FinancialTransaction> FinancialTransactions { get; set; }

        #endregion
    }
}
