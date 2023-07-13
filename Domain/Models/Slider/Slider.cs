#region Using

using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion
namespace Domain.Models.Slider
{
    public class Slider
    {
        #region properties

        [Key]
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

        [MaxLength(50)]
        public string SliderImageName { get; set; }

        [Display(Name = " کد رنگ ")]
        public string ColorCode { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDatetDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDelete { get; set; }

        public int Priority { get; set; }

        public string LinkTitle { get; set; }

        #endregion

        #region Relations

        #endregion
    }
}
