#region Using

using Application.Extensions;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

namespace ParsaWorkShop.Areas.User.Controllers
{
    public class HomeController : UserPanelBaseController
    {
        #region Ctor

        private IUserService _userservice;

        private readonly IProductService _productService;

        public HomeController(IUserService userService, IProductService productService)
        {
            _userservice = userService;
            _productService = productService;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            #region properties

            var model = await _productService.UserPanelDashboardViewModel(User.GetUserId());

            #endregion

            return View(model);
        }

        #endregion

    }
}
