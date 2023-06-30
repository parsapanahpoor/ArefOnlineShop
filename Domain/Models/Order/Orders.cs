using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class Orders
    {

        [Key]
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public int Userid { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsFinally { get; set; }

        [ForeignKey("Locations")]
        public int? LocationID { get; set; }

        #region Navigations

        public Users.User User { get; set; }
        public Users.Locations Locations { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public List<FinancialTransaction> FinancialTransactions { get; set; }

        #endregion

    }
}
