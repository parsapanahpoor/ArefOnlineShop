#region Usings

using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

#endregion

namespace Domain.Models.SiteSetting
{
    public class SiteSetting : BaseEntity
    {
        #region properties

        [Display(Name = "متن کپی رایت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CopyRightText { get; set; }

        [Display(Name = "تایم  ارسال اس ام اس ")]
        public int SendSMSTimer { get; set; }

        public string SiteDomain { get; set; }

        [Display(Name = "موجودی صندوق سایت")]
        public int SiteCashDesk { get; set; }

        #endregion
    }
}

