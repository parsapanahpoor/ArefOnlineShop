using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.ViewModels.SiteSide.Account
{
    public class ActiveMobileByActivationCodeViewModel
    {
        #region Properties

        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [DataType(DataType.MultilineText)]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "موبایل وارد شده معتبر نمی باشد")]
        public string Mobile { get; set; }

        [Display(Name = "کد فعالسازی ارسال شده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string MobileActiveCode { get; set; }

        #endregion 
    }
    public enum ActiveMobileByActivationCodeResult
    {
        Success,
        AccountNotFound
    }
}
