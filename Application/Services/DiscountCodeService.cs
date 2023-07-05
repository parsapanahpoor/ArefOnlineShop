#region Using

using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Application.Services
{
    public class DiscountCodeService : IDiscountCodeService
    {
        #region Ctor 

        private readonly IDiscountCodeRepository _discountCode;

        public DiscountCodeService(IDiscountCodeRepository discountCode)
        {
                _discountCode = discountCode;
        }

        #endregion

        #region Admin Side 

        #endregion
    }
}
