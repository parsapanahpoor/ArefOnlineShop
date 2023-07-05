#region Using

using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.Models.FavoriteProducts
{
    public sealed class FavoriteProducts : BaseEntity
    {
        #region properties

        public int UserId { get; set; }

        public int ProductId { get; set; }

        #endregion
    }
}
