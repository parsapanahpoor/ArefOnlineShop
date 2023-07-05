#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.ViewModels.UserPanel.FavoriteProducts
{
    public class FavoriteProductsUserPanelSideViewModel
    {
        #region properties

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        public string SeoTitle{ get; set; }

        public DateTime CreateDate { get; set; }

        #endregion
    }
}
