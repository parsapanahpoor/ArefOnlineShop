#region Using

using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.Models.Product
{
    public class ProductColor : BaseEntity
    {
        #region properties

        public string ColorTitle { get; set; }

        public string ColorImage { get; set; }

        #endregion
    }
}
