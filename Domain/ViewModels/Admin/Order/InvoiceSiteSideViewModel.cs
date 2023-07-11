using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.SiteSide.Order
{
    public class InvoiceSiteSideViewModel
    {
        #region proeprties

        public Domain.Models.Order.Orders Order { get; set; }

        public List<InvoiceOrderDetailSiteSideViewModel> InvoiceOrderDetails { get; set; }

        #endregion
    }

    public class InvoiceOrderDetailSiteSideViewModel
    {
        public int OrderDetailID { get; set; }

        public InvoiceProductSiteSideViewModel Product { get; set; }

        public int Count { get; set; }

        public bool IsReturend { get; set; }

        public InvoiceColorSiteSideViewModel InvoiceColor { get; set; }

        public InvoiceSizeSiteSideViewModel InvoiceSize { get; set; }
    }

    public class InvoiceProductSiteSideViewModel
    {
        public int ProductId { get; set; }

        public string ProductTitle { get; set; }

        public string ProductImage{ get; set; }

        public decimal Price { get; set; }
    }

    public class InvoiceColorSiteSideViewModel
    {
        public int ColorId { get; set; }

        public string ColorName { get; set; }

        public string ColorCode { get; set; }
    }

    public class InvoiceSizeSiteSideViewModel
    {
        public int SizeId { get; set; }

        public string SizeName { get; set; }
    }
}
