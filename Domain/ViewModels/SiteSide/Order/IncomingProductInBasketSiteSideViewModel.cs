using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.SiteSide.Order
{
    public class IncomingProductInBasketSiteSideViewModel
    {
        #region properties

        public int? id { get; set; }
        
        public int? selectColor { get; set; }

        public int? selectSize { get; set; }

        #endregion
    }
}
