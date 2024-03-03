using Domain.Models.Order;
using Domain.Models.Users;
using System.Collections.Generic;

namespace Domain.ViewModels.SiteSide.Order
{
    public class FinalInvoiceSiteSideDTO
    {
        #region properties

        public User UserInfo { get; set; }

        public Models.Order.Orders Order{ get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        public Locations Locations { get; set; }

        #endregion
    }
}
