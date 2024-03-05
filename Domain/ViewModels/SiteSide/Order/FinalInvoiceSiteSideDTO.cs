using Domain.Models.Order;
using Domain.Models.Product;
using Domain.Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ViewModels.SiteSide.Order
{
    public class FinalInvoiceSiteSideDTO
    {
        #region properties

        public User UserInfo { get; set; }

        public Models.Order.Orders Order{ get; set; }

        public List<OrderDetailDTO> OrderDetails { get; set; }

        public Locations Locations { get; set; }

        #endregion
    }

    public class OrderDetailDTO
    {
        public int OrderDetailID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public bool IsReturend { get; set; }

        public string ColorName { get; set; }

        public string SizeName { get; set; }

        public Domain.Models.Product.Product Product { get; set; }
    }
}
