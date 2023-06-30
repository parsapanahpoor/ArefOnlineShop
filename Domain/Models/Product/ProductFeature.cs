using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Product
{
    public class ProductFeature
    {

        [Key]
        public int FeatureID { get; set; }
        public int ProductID { get; set; }

        [Display(Name = "عنوان ویژگی   ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(800, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FeatureTitle { get; set; }

        [Display(Name = " مقدار ویژگی   ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FeatureValue { get; set; }


        #region Relations

        public  Product Product { get; set; }

        #endregion

    }
}
