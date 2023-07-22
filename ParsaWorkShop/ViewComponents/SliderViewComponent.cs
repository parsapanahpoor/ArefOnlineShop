using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ParsaWorkShop.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        #region Ctor

        private readonly ISliderService _sliderService;

        public SliderViewComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            #region Slider

            var model = await _sliderService.GetListOfSlidersForShowInLanding();

            #endregion


            return View("Slider", model);
        }
    }
}
