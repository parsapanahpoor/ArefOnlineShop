#region Using

using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.Models.Discount
{
    public sealed class DiscountCodeSelectedFromUser : BaseEntity
    {
        #region prperties

        public int UserId { get; set; }

        public int DiscountId { get; set; }

        public int OrderId{ get; set; }

        #endregion
    }
}
