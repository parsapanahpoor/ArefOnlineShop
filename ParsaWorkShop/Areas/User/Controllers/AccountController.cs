using Application.Interfaces;
using Application.ViewModels;
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
    public class AccountController : Controller
    {
        #region Ctor
        private IUserService _userservice;
        public AccountController(IUserService userService)
        {
            _userservice = userService;
        }

        #endregion

        public IActionResult EditUser()
        {
            return View(_userservice.GetDataForEditProfileUser(User.Identity.Name));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(EditProfileViewModel profile)
        {
            if (!ModelState.IsValid)
                return View(profile);

            _userservice.EditProfile(User.Identity.Name, profile);

            return Redirect("/User/Home");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangeUserPasswordViewModel change)
        {
            string currentUserName = User.Identity.Name;

            if (!ModelState.IsValid)
                return View(change);

            if (!_userservice.CompareOldPassword(change.OldPassword, currentUserName))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نمیباشد");
                return View(change);
            }
            _userservice.ChangeUserPassword(currentUserName, change.Password);

            return Redirect("/User/Home");
        }

    }
}
