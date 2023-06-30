using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Slider
{
    public class Slider
    {

        [Key]
        public int SliderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Display(Name = " متن اول ")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FirstText { get; set; }

        [Display(Name = " متن دوم ")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string SecondeText { get; set; }

        [Display(Name = " متن سوم ")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ThirdText { get; set; }

        [MaxLength(350)]
        public string Link { get; set; }

        [MaxLength(50)]
        public string BlogImageName { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDatetDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }


        #region Relations
        public  User Users { get; set; }
        #endregion

    }
}
