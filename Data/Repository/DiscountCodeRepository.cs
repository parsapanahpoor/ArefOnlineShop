#region Using

using Data.Context;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Data.Repository
{
    public class DiscountCodeRepository : IDiscountCodeRepository
    {
        #region Ctor 

        private readonly ParsaWorkShopContext _context;

        public DiscountCodeRepository(ParsaWorkShopContext context)
        {
                _context = context;
        }

        #endregion

        #region Admin Side 



        #endregion
    }
}
