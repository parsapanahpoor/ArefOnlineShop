using Application.Interfaces;
using Application.Security;
using Domain.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using System.Threading.Tasks;

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    [PermissionChecker(7)]
    public class SliderController : AdminBaseController
    {
        #region Using

        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
                _sliderService = sliderService;
        }

        #endregion

        #region List OF Sliders

        public async Task<IActionResult> ListOfSliders()
        {
            return View(await _sliderService.FillListOfSliersAdminSideViewModel()); ;
        }

        #endregion

        #region Create Slider 

        [HttpGet]
        public async Task<IActionResult> CreateSlider()
        {
            return View();
        }

        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSlider(CreateSliderAdminSideViewModel model , IFormFile imgBlogUp)
        {
            #region Model State Validation 

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            #region Add Slider To The Data Base

            var res = await _sliderService.AddSliderAdminSide(model , imgBlogUp);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfSliders));
            }

            #endregion

            return View(model);
        }

        #endregion

        #region Edit Slider 

        [HttpGet]
        public async Task<IActionResult> EditSlider(int sliderId)
        {
            var model = await _sliderService.FillEditSliderViewModel(sliderId);
            if(model == null) return NotFound();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSlider(EditSliderViewModel model, IFormFile? imgBlogUp)
        {
            #region Model State Validation 

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            #region Add Slider To The Data Base

            var res = await _sliderService.EditSliderAdminSidel(model, imgBlogUp);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfSliders));
            }

            #endregion

            return View(model);
        }

        #endregion

        #region Delete Slider

        public async Task<IActionResult> DeleteSlider(int sliderId)
        {
            #region Delete Slider 

            var res = await _sliderService.DeleteSlider(sliderId);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfSliders));
            }

            #endregion

            TempData[ErrorMessage] = "عملیات باشکست مواجه شده است.";
            return RedirectToAction(nameof(ListOfSliders));
        }

        #endregion
    }
}
