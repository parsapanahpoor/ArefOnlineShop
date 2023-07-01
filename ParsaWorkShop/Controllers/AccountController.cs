#region Usings

using Application.Convertors;
using Application.Genarator;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.SiteSide.Account;
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

            TempData[SuccessMessage] = "Hello";

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

        [HttpGet("Login")]
        [RedirectHomeIfLoggedInActionFilter]
        public async Task<ActionResult> Login(string ReturnUrl = "/")
        {
            ViewBag.Return = ReturnUrl;

            return View();
        }

        [HttpPost("Login"), ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel login, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "مقادیر وارد شده معتبر نمی باشد .";
                return View(login);
            }

            var result = await _userService.CheckUserForLogin(login);

            switch (result)
            {
                case LoginResult.UserNotFound:
                    TempData[ErrorMessage] = "کاربری با اطلاعات وارد شده یافت نشد .";
                    break;
                case LoginResult.UserIsBan:
                    TempData[WarningMessage] = "دسترسی شما به سایت محدود شده است .";
                    break;
                case LoginResult.MobileNotActivated:
                    TempData[WarningMessage] = "حساب کاربری شما فعال نشده است";
                    return RedirectToAction("ActiveUserByMobileActivationCode", new { Mobile = login.Mobile, Resend = true });

                case LoginResult.Success:

                    #region Login User

                    // var user = await _userService.GetUserByEmail(login.Mobile);
                    var user = await _userService.GetUserByMobile(login.Mobile);

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties { IsPersistent = login.RememberMe };

                    await HttpContext.SignInAsync(principal, properties);

                    #endregion

                    #region Return To URL

                    if (!string.IsNullOrEmpty(login.ReturnUrl))
                    {
                        return Redirect(login.ReturnUrl);
                    }

                    #endregion

                    return RedirectToAction("Index", "Home");
            }

            return View(login);
        }


        #endregion

        #region Active mobile user

        [HttpGet("Active-mobile/{Mobile}/{Resend?}")]
        public async Task<IActionResult> ActiveUserByMobileActivationCode(string Mobile, bool Resend = false)
        {
            #region Model State Validation 

            if (Mobile == null)
            {
                return NotFound();
            }

            #endregion

            #region Is Exist User 

            if (await _userService.IsExistUserByMobile(Mobile) == false)
            {
                return NotFound();
            }

            #endregion

            #region Resend SMS

            if (Resend)
            {
                await _userService.ResendActivationCodeSMS(Mobile);
            }

            #endregion

            #region Get User By User ID

            var user = await _userService.GetUserByMobile(Mobile);

            #endregion

            #region Time Counter Initilize

            var SiteSettingSMSTimer = 2;

            DateTime expireMinut = user.ExpireMobileSMSDateTime.Value.AddMinutes(SiteSettingSMSTimer);

            var TimerMinut = expireMinut - DateTime.Now;

            ViewBag.Time = TimerMinut.TotalMinutes * 60;

            ViewBag.Mobile = Mobile;

            #endregion

            return View();
        }

        [HttpPost("Active-mobile/{Mobile}/{Resend?}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ActiveUserByMobileActivationCode(ActiveMobileByActivationCodeViewModel activeMobileByActivationCodeViewModel)
        {
            #region Active User Mobile

            if (ModelState.IsValid)
            {
                var result = await _userService.ActiveUserMobile(activeMobileByActivationCodeViewModel);
                switch (result)
                {
                    case ActiveMobileByActivationCodeResult.Success:
                        TempData[SuccessMessage] = "حساب کاربری شما با موفقیت فعال شد!";
                        return RedirectToAction(nameof(Login));

                    case ActiveMobileByActivationCodeResult.AccountNotFound:
                        TempData[ErrorMessage] = "اطلاعات وارد شده اشتباه می باشد!";
                        return RedirectToAction("ActiveUserByMobileActivationCode", new { Mobile = activeMobileByActivationCodeViewModel.Mobile, Resend = false });
                }
            }

            #endregion

            return View(activeMobileByActivationCodeViewModel);
        }

        #endregion

        #region forgot password

        [HttpGet("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("ForgotPassword"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgetPasswordViewModel forgot)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RecoverUserPassword(forgot);
                switch (result)
                {
                    case ForgotPasswordResult.VerificationSmsFaildFromParsGreen:
                        TempData[ErrorMessage] = " کد تایید جدید برای شما ارسال نشد!";
                        TempData[ErrorMessage] = "لطفا با پشتیبانی سایت تماس بگیرید!";
                        ModelState.AddModelError("Mobile", "لطفا با پشتیبانی سایت تماس بگیرید");
                        break;
                    case ForgotPasswordResult.NotFound:
                        TempData[WarningMessage] = "کاربر مورد نظر یافت نشد";
                        break;
                    case ForgotPasswordResult.FailSendEmail:
                        TempData[WarningMessage] = "در ارسال ایمیل مشکلی رخ داد";
                        break;
                    case ForgotPasswordResult.UserIsBlocked:
                        TempData[ErrorMessage] = "حساب کاربری شما بسته شده است!";
                        break;
                    case ForgotPasswordResult.SuccessSendEmail:
                        TempData[WarningMessage] = "کد جدید برای شما ارسال شد";
                        return RedirectToAction("ResetPassword", "Account", new { mobile = forgot.Mobile });
                    case ForgotPasswordResult.Success:
                        TempData[SuccessMessage] = "کد تایید جدید برای شما ارسال شد";
                        return RedirectToAction("ResetPassword", "Account", new { mobile = forgot.Mobile });
                }
            }

            return View(forgot);
        }

        #endregion

        #region reset password

        [HttpGet("reset-pass/{mobile}")]
        public async Task<IActionResult> ResetPassword(string mobile, bool resend)
        {
            #region Send Model To View

            ViewBag.Mobile = mobile;

            var user = await _userService.GetUserByMobile(mobile);

            if (user == null) return NotFound();

            if (resend)
            {
                await _userService.ResendActivationCodeSMS(mobile);
            }

            var SiteSettingSMSTimer = 2;
            DateTime expireMinut = user.ExpireMobileSMSDateTime.Value.AddMinutes(SiteSettingSMSTimer);

            var TimerMinut = expireMinut - DateTime.Now;

            ViewBag.Time = TimerMinut.TotalMinutes * 60;


            #endregion

            return View();
        }

        [HttpPost("reset-pass/{mobile}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string mobile, ResetPasswordViewModel reset)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.ResetUserPassword(reset, mobile);
                switch (res)
                {
                    case ResetPasswordResult.NotFound:
                        TempData[WarningMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                        return Redirect("/");
                    case ResetPasswordResult.WrongActiveCode:
                        TempData[ErrorMessage] = "کد تایید وارد شده صحیح نمی باشد";
                        break;
                    case ResetPasswordResult.Success:
                        TempData[SuccessMessage] = "کلمه عبور شما با موفقیت تغییر پیدا کرد";
                        await HttpContext.SignOutAsync();
                        return RedirectToAction("Login", "Account", new { area = "" });
                }
            }
            return View(reset);
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
