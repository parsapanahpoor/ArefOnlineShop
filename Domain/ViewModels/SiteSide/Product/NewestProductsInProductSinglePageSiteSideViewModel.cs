using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.SiteSide.Product
{
    public class NewestProductsInProductSinglePageSiteSideViewModel
    {
        #region properties

        public int ProductId { get; set; }

        public string Title { get; set; }

        public string ImageName { get; set; }

        public int OldPrice { get; set; }

        public int Price { get; set; }

        #endregion
    }
}
