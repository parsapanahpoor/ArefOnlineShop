#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.ViewModels.Admin.DiscountCode
{
    public class ListOfDiscountSelectedFromUserAdminSideViewModel
    {
        #region Properties

        public int OrdeId { get; set; }

        public int DiscountId { get; set; }

        public UserSelectedDiscount User { get; set; }

        public DateTime CreateDate { get; set; }

        #endregion
    }

    public class UserSelectedDiscount
    {
        #region properties

        public int UserId { get; set; }

        public string Mobile { get; set; }

        public string Username{ get; set; }

        public string UserAvatar { get; set; }

        #endregion
    }
}
