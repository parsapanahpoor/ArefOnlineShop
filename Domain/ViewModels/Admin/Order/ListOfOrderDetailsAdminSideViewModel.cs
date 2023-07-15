using Domain.Models.Order;
using Domain.Models.Product;

namespace Domain.ViewModels.Admin.Order
{
    public class ListOfOrderDetailsAdminSideViewModel
    {
        #region properties

        public OrderDetails OrderDetails { get; set; }

        public ProductsSize ProductsSize { get; set; }

        public ProductColor ProductColor { get; set; }

        #endregion
    }
}
