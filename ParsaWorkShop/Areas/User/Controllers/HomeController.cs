using Application.Interfaces;
using Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class HomeController : Controller
    {
        #region Ctor
        private IUserService _userservice;
        public HomeController(IUserService userService)
        {
            _userservice = userService;
        }
        #endregion


        public IActionResult Index()
        {
            var user = _userservice.GetUserByUserName(User.Identity.Name);
            return View(user);
        }
    }
}
