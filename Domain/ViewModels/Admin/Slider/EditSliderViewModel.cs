using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.ViewModels.Admin.Slider
{
    public class EditSliderViewModel
    {
        #region properties

        public int SliderId { get; set; }

        [Display(Name = " متن اول ")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FirstText { get; set; }

        [Display(Name = " متن دوم ")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string SecondeText { get; set; }

        [Display(Name = " متن سوم ")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ThirdText { get; set; }

        public string Link { get; set; }

        [Display(Name = " کد رنگ ")]
        public string ColorCode { get; set; }

        public string ImageName { get; set; }

        #endregion
    }
}
