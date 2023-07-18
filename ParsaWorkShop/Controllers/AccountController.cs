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
                TempData[SuccessMessage] = "please input informations";

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
                TempData[ErrorMessage] = "informations are not valid ";
                return View(register);
            }

            #endregion

            #region Register User Methods

            var result = await _userService.RegisterUser(register);

            switch (result)
            {
                case RegisterUserResult.MobileExist:
                    TempData[ErrorMessage] = "The entered mobile phone number is duplicate";
                    TempData[InfoMessage] = "If you have already registered on the site, use the option to enter the site";
                    ModelState.AddModelError("UserName", "The entered mobile phone number is duplicate");
                    break;

                case RegisterUserResult.UsernameExist:
                    TempData[ErrorMessage] = "The entered username is duplicate.";
                    ModelState.AddModelError("Mobile", "The entered username is duplicate.");
                    break;

                case RegisterUserResult.SiteRoleNotAccept:
                    TempData[ErrorMessage] = " Site rules must be accepted";
                    break;

                case RegisterUserResult.Success:
                    TempData[SuccessMessage] = "Your registration has been successfully completed.";
                    TempData[InfoMessage] = $"sms was sent to {register.Mobile}";

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
                TempData[ErrorMessage] = "\r\nThe entered values ​​are not valid.";
                return View(login);
            }

            var result = await _userService.CheckUserForLogin(login);

            switch (result)
            {
                case LoginResult.UserNotFound:
                    TempData[ErrorMessage] = "User with entered information was not found.";
                    break;
                case LoginResult.UserIsBan:
                    TempData[WarningMessage] = "Your access to the site is limited.";
                    break;
                case LoginResult.MobileNotActivated:
                    TempData[WarningMessage] = "\r\nYour account has not been activated";
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

                    TempData[SuccessMessage] = "Login Successfully";
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
                        TempData[SuccessMessage] = "You Profile Active Successfully";
                        return RedirectToAction(nameof(Login));

                    case ActiveMobileByActivationCodeResult.AccountNotFound:
                        TempData[ErrorMessage] = "This is problem with Input Informations";
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
                        TempData[ErrorMessage] = " The new verification code was not sent to you!";
                        TempData[ErrorMessage] = "Please contact site support!";
                        ModelState.AddModelError("Mobile", "Please contact site support");
                        break;
                    case ForgotPasswordResult.NotFound:
                        TempData[WarningMessage] = "The desired user was not found";
                        break;
                    case ForgotPasswordResult.FailSendEmail:
                        TempData[WarningMessage] = "There was a problem sending the email";
                        break;
                    case ForgotPasswordResult.UserIsBlocked:
                        TempData[ErrorMessage] = "Your account has been closed!";
                        break;
                    case ForgotPasswordResult.SuccessSendEmail:
                        TempData[WarningMessage] = "The new code has been sent to you";
                        return RedirectToAction("ResetPassword", "Account", new { mobile = forgot.Mobile });
                    case ForgotPasswordResult.Success:
                        TempData[SuccessMessage] = "A new verification code has been sent to you";
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
                        TempData[WarningMessage] = "A user with the entered profile was not found";
                        return Redirect("/");
                    case ResetPasswordResult.WrongActiveCode:
                        TempData[ErrorMessage] = "The verification code entered is not correct";
                        break;
                    case ResetPasswordResult.Success:
                        TempData[SuccessMessage] = "Your password has been successfully changed";
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
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Login));
            }

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
