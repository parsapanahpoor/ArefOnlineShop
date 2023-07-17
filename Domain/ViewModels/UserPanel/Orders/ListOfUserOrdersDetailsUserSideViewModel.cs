using Domain.Models.Order;
using Domain.Models.Product;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.UserPanel.Orders
{
    public class ListOfUserOrdersDetailsUserSideViewModel
    {
        #region properties

        public OrderDetails OrderDetails { get; set; }

        public Locations Locations { get; set; }

        public ProductColor ProductColor { get; set; }

        public Product Product { get; set; }

        public ProductsSize ProductsSize { get; set; }

        public Domain.Models.Order.Orders Order { get; set; }

        #endregion
    }
}
