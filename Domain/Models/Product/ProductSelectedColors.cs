using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Product
{
    public sealed class ProductSelectedColors : BaseEntity
    {
        #region properties

        public int ProductId { get; set; }

        public int ColorId { get; set; }

        #endregion
    }
}
