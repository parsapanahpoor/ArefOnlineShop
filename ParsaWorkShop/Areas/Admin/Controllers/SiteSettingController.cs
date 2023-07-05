﻿#region Using

using AngleSharp.Css.Values;
using Application.Interfaces;
using Application.Services;
using Domain.ViewModels.Admin.SiteSetting;
using Domain.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    public class SiteSettingController : AdminBaseController
    {
        #region Ctor

        private readonly ISiteSettingService _siteSettingService;

        public SiteSettingController(ISiteSettingService siteSettingService)
        {
                _siteSettingService = siteSettingService;
        }

        #endregion

        #region Product Color 

        #region List Of Product Colors

        public async Task<IActionResult> ListOfProductColors()
        {
            return View(await _siteSettingService.FillListOfProductColorsAdminSideViewModel());
        }

        #endregion

        #region Create Colors

        [HttpGet]
        public async Task<IActionResult> CreateColors()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateColors(CreateColorAdminSideViewModel model, IFormFile imgBlogUp)
        {
            #region Model State Validation 

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            #region Add Slider To The Data Base

            var res = await _siteSettingService.AddColorAdminSide(model, imgBlogUp);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfProductColors));
            }

            #endregion

            return View(model);
        }

        #endregion

        #region Edit Color 

        [HttpGet]
        public async Task<IActionResult> EditColor(int id)
        {
            #region Fill Model

            var model = await _siteSettingService.FillEditColorAdminSideViewModel(id);
            if (model == null) return NotFound();

            #endregion

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditColor(EditColorAdminSideViewModel model, IFormFile? imgBlogUp)
        {
            #region Model State Validation 

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            #region Add Color To The Data Base

            var res = await _siteSettingService.EditColorAdminSidel(model, imgBlogUp);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfProductColors));
            }

            #endregion

            return View(model);
        }

        #endregion

        #region Delete Color

        public async Task<IActionResult> DeleteColor(int id)
        {
            #region Delete Color

            var res = await _siteSettingService.DeleteColor(id);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfProductColors));
            }

            #endregion

            TempData[ErrorMessage] = "عملیات باشکست مواجه شده است.";
            return RedirectToAction(nameof(ListOfProductColors));
        }

        #endregion

        #endregion
    }
}
