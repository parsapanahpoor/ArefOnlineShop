#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.ViewModels.Admin.DiscountCode
{
    public class ListOfDiscountCodeAdminSideViewModel
    {
        #region properties

        public int Id { get; set; }

        public string DiscountTitle { get; set; }

        public string Code { get; set; }

        public int DiscountPercentage { get; set; }

        public int CountOfUsage { get; set; }

        #endregion
    }
}
