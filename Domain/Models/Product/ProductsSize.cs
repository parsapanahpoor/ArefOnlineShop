﻿#region Using

using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.Models.Product
{
    public class ProductsSize : BaseEntity
    {
        #region properties

        public string SizeTitle { get; set; }

        public int Priority { get; set; }

        #endregion

        #region Relations

        public List<ProductSelectedSize> ProductSelectedSizes { get; set; }

        #endregion
    }
}
