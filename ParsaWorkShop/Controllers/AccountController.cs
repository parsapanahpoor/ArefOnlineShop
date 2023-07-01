#region Usings

using Application.Convertors;
using Application.Genarator;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ParsaWorkShop.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

#endregion

namespace ParsaWorkShop.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region Ctor

        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Register

        [Route("Register")]
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View(register);
            }

            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }

            if (_userService.IsExistPhoneNumber(FixedText.FixEmail(register.PhoneNumber)))
            {
                ModelState.AddModelError("PhoneNumber", "شماره ی وارد شده معتبر نمی باشد ");
                return View(register);
            }

            _userService.AddUerByService(register);

            return Redirect("/Login");
        }

        #endregion

        #region login

        [Route("Login")]
        public ActionResult Login(string ReturnUrl = "/")
        {
            ViewBag.Return = ReturnUrl;

            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginViewModel login, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userService.LoginUser(login);

            if (user != null)
            {
                #region SetCoockie

                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);

                    return Redirect(ReturnUrl);
                }

                #endregion
                else
                {
                    ModelState.AddModelError("phoneNumber", "حساب کاربری شما فعال نمی باشد");
                }
            }
            ModelState.AddModelError("phoneNumber", "کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
        }


        #endregion

        #region Logout

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion

        #region CheckUserRoleForLogin

        public IActionResult ManageUserForLogin()
        {
            List<int> UserRoles = _userService.GetUsersRoles(User.Identity.Name);

            if (UserRoles.Any() == false)
            {
                return Redirect("/User/Home/Index");
            }
            else
            {
                if (UserRoles.Contains(3))
                {
                    return Redirect("/Admin/Home/Index");
                }
            }

            return View();
        }

        #endregion
    }
}
