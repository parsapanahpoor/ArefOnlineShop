#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.ViewModels.Admin.DiscountCode
{
    public class CreateDiscoutCodeAdminSideViewModel
    {
        #region properties

        public string DiscountTitle { get; set; }

        public int DiscountPercentage { get; set; }

        #endregion
    }
}
