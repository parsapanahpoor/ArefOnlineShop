using Application.Interfaces;
using Domain.Models.AboutUs;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    public class AboutUsController : AdminBaseController
    {
        #region Ctor

        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        #endregion

        #region Add Or Edit About Us

        [HttpGet]
        public async Task<IActionResult> AddOrEditAboutUs(CancellationToken cancellation = default)
        {
            return View(await _aboutUsService.GetAboutUs(cancellation));
        }

        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditAboutUs(AboutUs aboutUs ,CancellationToken cancellation = default)
        {
            #region Ad Or Edit About Us

            if (ModelState.IsValid)
            {
                var res = await _aboutUsService.AddOrEditAboutUs(aboutUs , cancellation);
                if (res)
                {
                    TempData[SuccessMessage] ="عملیات باموفقیت انجام شده است." ;
                    return RedirectToAction("Index" , "Home" , new { area = "Admin"});
                }
            }

            #endregion

            TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
            return View(await _aboutUsService.GetAboutUs(cancellation));
        }

        #endregion
    }
}
