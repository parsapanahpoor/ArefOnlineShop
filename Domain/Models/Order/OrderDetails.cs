using Domain.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailID { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public bool IsReturend { get; set; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }

        #region Navigations

        public Orders Order { get; set; }
        public Product.Product Product { get; set; }
        public List<ReturnedProducts> ReturnedProducts { get; set; }

        #endregion
    }
}
