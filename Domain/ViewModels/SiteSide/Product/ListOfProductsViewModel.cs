#region Using

using Domain.ViewModels.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;

#endregion

namespace Domain.ViewModels.SiteSide.Product
{
    public class ListOfProductsViewModel : BasePaging<Domain.Models.Product.Product>
    {
        #region properties

        public List<int> ColorsId { get; set; }

        public List<int> CategoriesId { get; set; }

        public List<int> SizesId { get; set; }

        public int? MaxPrice { get; set; }

        public int? MinPrice { get; set; }

        public FilterStatus Status { get; set; }

        #endregion
    }

    public class ListOfCategoriesForShowInListOfProducts
    {
        #region properties

        public int CategoryId { get; set; }

        public string CategoryTitle { get; set; }

        #endregion
    }

    public class ListOfColorsForShowInListOfProducts
    {
        #region properties

        public int ColorId { get; set; }

        public string ColorTitle { get; set; }

        #endregion
    }

    public class ListOfSizesForShowInListOfProducts
    {
        #region properties

        public int SizesId { get; set; }

        public string SizesTitle { get; set; }

        #endregion
    }

    public enum FilterStatus
    {
        [Display(Name = "همه ")]
        All,
        [Display(Name = "تاریخ ")]
        CreateDate,
        [Display(Name = "بیشترین قیمت ")]
        MaxPrice,
        [Display(Name = "کمترین قیمت ")]
        MinPrice,
        [Display(Name = "تخفیف دارها ")]
        Offer
    }
}
