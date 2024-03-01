using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string BirthDay { get; set; }

        [Display(Name = "شماره تلفن ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]

        public string Mobile { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }

        public bool SiteRoles { get; set; }
    }

    #region Login 

    public class LoginViewModel
    {
        [Display(Name = "تلفن همراه ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Mobile { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }

    public enum LoginResult
    {
        Success,
        UserNotFound,
        UserIsBan,
        MobileNotActivated
    }

    #endregion

    public class ForgotPasswordViewModel
    {
        [Display(Name = "موبایل ")]
        [Required(ErrorMessage = "لطفا{0} را وارد نمایید...")]
        public string Mobile { get; set; }
    }
    public class RecoverPasswordViewModel
    {

        public int Userid { get; set; }

        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا{0} را وارد نمایید...")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا{0} را وارد نمایید...")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور وارد شده مغایرت دارند.")]
        public string RePassword { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا{0} را وارد نمایید...")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا{0} را وارد نمایید...")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا{0} را وارد نمایید...")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور وارد شده مغایرت دارند.")]
        public string RePassword { get; set; }

    }

    public enum RegisterUserResult
    {
        Success,
        MobileExist,
        SiteRoleNotAccept,
        UsernameExist
    }
}
