#region Using

using Domain.Models.Common;
using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string ColorFarsiTitle { get; set; }

        public string ColorImage { get; set; }

        public string ColorCode { get; set; }

        #endregion

        #region Relations

        public List<ProductSelectedColors> ProductSelectedColors { get; set; }

        #endregion
    }
}
