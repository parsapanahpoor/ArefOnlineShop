#region Using

using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace ParsaWorkShop.Areas.User.Controllers
{
    public class HomeController : UserPanelBaseController
    {
        #region Ctor

        private IUserService _userservice;
        public HomeController(IUserService userService)
        {
            _userservice = userService;
        }

        #endregion

        #region Index

        public IActionResult Index()
        {
            var user = _userservice.GetUserByUserName(User.Identity.Name);

            return View(user);
        }

        #endregion

    }
}
