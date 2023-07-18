#region Usings

using Application.Interfaces;
using Domain.ViewModels.SiteSide.ContactUs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsaWorkShop.Models;
using ParsaWorkShop.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace ParsaWorkShop.Controllers
{
    public class HomeController : SiteBaseController
    {
        #region Ctor

        private readonly ILogger<HomeController> _logger;
        private readonly ISiteSettingService _siteSettingService;
        private readonly IUsersCommentAboutSiteService _usersCommentAboutSiteService;

        public HomeController(ILogger<HomeController> logger , ISiteSettingService siteSettingService, IUsersCommentAboutSiteService usersCommentAboutSiteService)
        {
            _logger = logger;
            _siteSettingService = siteSettingService;
            _usersCommentAboutSiteService = usersCommentAboutSiteService;
        }

        #endregion

        #region Index

        //[ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<IActionResult> Index()
        {
            #region Fill Model

            var model = await _siteSettingService.FillIndexPageViewModel();

            #endregion

            return View(model);
        }

        #endregion

        #region privacy

        public IActionResult Privacy()
        {
            return View();
        }

        #endregion

        #region Error

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Contact Us 

        [HttpGet]
        public async Task<IActionResult> ContactUs()
        {
            return View();
        }

        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsSiteSideViewModel model)
        {
            #region Model State Validation 

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
                return View();
            }

            #endregion

            #region Add contact Us 

            var res = await _usersCommentAboutSiteService.AddContactUs(model);
            if (res)
            {
                TempData[SuccessMessage] = "Your Operation Has been Successfully";
                return RedirectToAction(nameof(Index));
            }

            #endregion

            TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
            return View();
        }

        #endregion

        #region About Us

        public async Task<IActionResult> AboutUs()
        {
            return View();
        }

        #endregion
    }
}
