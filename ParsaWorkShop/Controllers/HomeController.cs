﻿#region Usings

using Application.Extensions;
using Application.Interfaces;
using Application.Services;
using Domain.Models.Permissions;
using Domain.ViewModels.SiteSide.ContactUs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Extensions.Logging;
using ParsaWorkShop.Models;
using ParsaWorkShop.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
        private readonly IAboutUsService _aboutUsService;
        private readonly IFavoriteProductsService _favoriteProductsService;



        public HomeController(ILogger<HomeController> logger,
                              ISiteSettingService siteSettingService,
                              IUsersCommentAboutSiteService usersCommentAboutSiteService,
                              IAboutUsService aboutUsService, IFavoriteProductsService favoriteProductsService)
        {
            _logger = logger;
            _siteSettingService = siteSettingService;
            _usersCommentAboutSiteService = usersCommentAboutSiteService;
            _aboutUsService = aboutUsService;
            _favoriteProductsService = favoriteProductsService;
        }

        #endregion

        #region Index

        //[ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<IActionResult> Index(bool? permission)
        {
            if (permission == true)
            {
                ViewData[ErrorMessage] = "you dont have permission";
            }

            #region Fill Model

            var model = await _siteSettingService.FillIndexPageViewModel(User.Identity.IsAuthenticated ? User.GetUserId() : null);

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

        [HttpPost, ValidateAntiForgeryToken]
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

        public async Task<IActionResult> AboutUs(CancellationToken cancellation = default)
        {
            #region Fill Model

            var model = await _siteSettingService.FillIndexPageViewModel(User.Identity.IsAuthenticated ? User.GetUserId() : null);

            ViewData["AboutUs"] = await _aboutUsService.GetAboutUs(cancellation);

            #endregion

            return View(model);
        }

        #endregion

        #region Test For Payment result page 

        public async Task<IActionResult> Test()
        {
            return View();
        }

        #endregion

        #region Add To Favorite

        [Authorize, HttpGet]
        public async Task<IActionResult> AddToFavorite(int productId, string url)
        {
            var res = await _favoriteProductsService.AddorRemoveProductFromFavorite(productId, User.GetUserId());
            if (res)
            {
                TempData[SuccessMessage] = "Success";
                return Redirect(url);
            }

            TempData[ErrorMessage] = "Faild";
            return Redirect(url);
        }

        #endregion
    }
}
