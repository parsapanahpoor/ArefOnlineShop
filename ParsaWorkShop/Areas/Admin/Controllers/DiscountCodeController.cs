#region Using

using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    public class DiscountCodeController : AdminBaseController
    {
        #region Ctor 

        private readonly IDiscountCodeService _discountCodeService;

        public DiscountCodeController(IDiscountCodeService discountCodeService)
        {
                _discountCodeService = discountCodeService;
        }

        #endregion

        #region List Of Discount Codes



        #endregion
    }
}
