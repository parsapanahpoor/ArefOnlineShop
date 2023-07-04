using Application.Interfaces;
using Domain.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using System.Threading.Tasks;

namespace ParsaWorkShop.Areas.Admin.Controllers
{
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
    }
}
