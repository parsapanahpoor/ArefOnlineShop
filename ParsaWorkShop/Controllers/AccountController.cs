#region Usings

using Application.Convertors;
using Application.Genarator;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ParsaWorkShop.HttpManager;
using ParsaWorkShop.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        [HttpGet("Register")]
        [RedirectHomeIfLoggedInActionFilter]
        public async Task<ActionResult> Register(string? mobile)
        {
            #region Redirect Mobile 

            if (!string.IsNullOrEmpty(mobile))
            {
                TempData[SuccessMessage] = "لطفا اطلاعات خواسته شده را جهت ادامه ی مراحل ثبت نام وارد نمایید.";

                return View(new RegisterViewModel()
                {
                    Mobile = mobile
                });
            }

            #endregion

            return View();
        }

        [HttpPost("Register"), ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel register)
        {
            #region Model State Validations

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "مقادیر وارد شده معتبر نمی باشد . ";
                return View(register);
            }

            #endregion

            #region Register User Methods

            var result = await _userService.RegisterUser(register);

            switch (result)
            {
                case RegisterUserResult.MobileExist:
                    TempData[ErrorMessage] = "تلفن همراه وارد شده تکراری می باشد";
                    TempData[InfoMessage] = "در صورتی که از قبل در سایت ثبت نام کردید از گزینه ی ورود به سایت استفاده کنید";
                    ModelState.AddModelError("Mobile", "تلفن همراه وارد شده تکراری می باشد");
                    break;

                case RegisterUserResult.SiteRoleNotAccept:
                    TempData[ErrorMessage] = "قوانین سایت باید پذیرفته شوند ";
                    break;

                case RegisterUserResult.Success:
                    TempData[SuccessMessage] = "ثبت نام شما با موفقیت انجام شد .";
                    TempData[InfoMessage] = $"پیامی  حاوی کد فعالسازی حساب کاربری به {register.Mobile} ارسال شد .";

                    return RedirectToAction("ActiveUserByMobileActivationCode", new { Mobile = register.Mobile });
            }

            #endregion

            return View(register);
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
