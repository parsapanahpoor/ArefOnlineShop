using Domain.Models.Common;
using Domain.ViewModels.SiteSide.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.SiteSide.SitSideBar
{
    public class SiteSideBarViewModel 
    {
        #region properties

        public List<ListOfProductCategoriesForShowInSiteSideBar> ListOfProductCategoriesForShowInSiteSideBar { get; set; }

        public InvoiceSiteSideViewModel? Invoices { get; set; }

        #endregion
    }

    public class ListOfProductCategoriesForShowInSiteSideBar
    {
        #region properties

        public int CategoryId { get; set; }

        public string CategoryTitle { get; set; }

        #endregion
    }
}
