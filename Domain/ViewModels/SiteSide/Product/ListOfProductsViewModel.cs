#region Using

using Domain.ViewModels.Common;
using System.Collections.Generic;

#endregion

namespace Domain.ViewModels.SiteSide.Product
{
    public class ListOfProductsViewModel : BasePaging<Domain.Models.Product.Product>
    {
        #region properties

        public List<int> ColorsId { get; set; }

        public List<int> CategoriesId { get; set; }

        public List<int> SizesId { get; set; }

        public int MaxPrice { get; set; }

        public int MinPrice { get; set; }

        #endregion
    }
}
