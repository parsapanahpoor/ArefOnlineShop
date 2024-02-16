using Domain.Models.Comment;
using Domain.Models.Product;
using Domain.Models.SizeHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.ViewModels.SiteSide.Product
{
    public class ProductDetailSiteSideViewModel
    {
        #region properties

        public int ProdcutId { get; set; }

        public string ProductTitle { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string ProductImageName { get; set; }

        public int? OfferPercent { get; set; }

        [Display(Name = "تخفیف وضعیت")]
        public bool? IsInOffer { get; set; }

        public int ProductCount { get; set; }

        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        public string Tags { get; set; }

        public DateTime CreateDate { get; set; }

        public List<ProductFeature> ProductFeatures { get; set; }

        public List<ProductGallery> ProductGallery { get; set; }

        public List<ProductColor> ProductColors { get; set; }

        public List<ProductsSize> ProductsSizes { get; set; }

        public List<ProductCategoryLinkable> ProductCategoryLinkable { get; set; }

        public List<Comment> Comments { get; set; }

        public List<SizeHelper> SizeHelpers { get; set; }

        #endregion
    }

    public class ProductCategoryLinkable
    {
        public int CategoryId { get; set; }

        public string CategoryTitle { get; set; }
    }
}
