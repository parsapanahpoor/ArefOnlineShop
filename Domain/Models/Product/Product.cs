using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Product
{
    public class Product
    {

        [Key]
        public int ProductID { get; set; }

        [Required]
        public int UserId { get; set; }

        [Display(Name = "عنوان محصول  ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ProductTitle { get; set; }

        [Display(Name = "شرح کوتاه  محصول ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string ShortDescription { get; set; }

        [Display(Name = "شرح کامل محصول ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string LongDescription { get; set; }

        [MaxLength(50)]
        public string ProductImageName { get; set; }

        public int? OfferPercent { get; set; }

        [Display(Name = "تخفیف وضعیت")]
        public bool? IsInOffer { get; set; }

        public int ProductCount { get; set; }

        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }

        [MaxLength(600)]
        public string Tags { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }


        #region Relations

        public  List<ProductSelectedCategory> ProductSelectedCategory { get; set; }
        public  List<ProductFeature> ProductFeatures { get; set; }
        public  List<ProductGallery> ProductGalleries { get; set; }
        public  List<Order.OrderDetails> OrderDetails { get; set; }
        public  List<Comment.Comment> Comments { get; set; }
        public  User Users { get; set; }

        #endregion



    }
}
