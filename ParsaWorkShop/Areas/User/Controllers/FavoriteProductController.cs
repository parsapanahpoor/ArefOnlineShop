#region Using

using Application.Extensions;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


#endregion

namespace ParsaWorkShop.Areas.User.Controllers
{
    public class FavoriteProductController : UserPanelBaseController
    {
        #region Ctor

        private readonly IFavoriteProductsService _favoriteProductsService;

        public FavoriteProductController(IFavoriteProductsService favoriteProductsService)
        {
            _favoriteProductsService = favoriteProductsService;
        }

        #endregion

        #region List OF Favorite Products

        [HttpGet]
        public async Task<IActionResult> ListOFFavoriteProducts()
        {
            return View(await _favoriteProductsService.FillFavoriteProductsUserPanelSideViewModel(User.GetUserId()));
        }

        #endregion
    }
}
