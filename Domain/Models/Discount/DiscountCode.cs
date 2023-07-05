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
    public sealed class DiscountCode : BaseEntity
    {
        #region property

        public string DiscountTitle { get; set; }

        public string Code { get; set; }

        public int DiscountPercentage { get; set; }

        #endregion
    }
}
