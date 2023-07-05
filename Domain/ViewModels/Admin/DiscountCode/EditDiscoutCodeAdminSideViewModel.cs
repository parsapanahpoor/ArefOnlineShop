#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion
namespace Domain.ViewModels.Admin.DiscountCode
{
    public class EditDiscoutCodeAdminSideViewModel
    {
        #region properties

        public int Id { get; set; }

        public string DiscountTitle { get; set; }

        public int DiscountPercentage { get; set; }

        #endregion
    }
}
