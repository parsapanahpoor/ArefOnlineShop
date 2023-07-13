#region Using

using Application.Interfaces;
using Application.Security;
using Domain.ViewModels.Admin.DiscountCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

#endregion

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    [PermissionChecker(1)]
    public class DiscountCodeController : AdminBaseController
    {
        #region Ctor 

        private readonly IDiscountCodeService _discountCodeService;

        public DiscountCodeController(IDiscountCodeService discountCodeService)
        {
                _discountCodeService = discountCodeService;
        }

        #endregion

        #region Discount Code 

        #region List Of Discount Codes

        [HttpGet]
        public async Task<IActionResult> ListOfDiscountCodes()
        {
            return View(await _discountCodeService.FillListOfDiscountCodeAdminSideViewModel());
        }

        #endregion

        #region Create Discount Code

        [HttpGet]
        public async Task<IActionResult> CreateDiscountCode()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDiscountCode(CreateDiscoutCodeAdminSideViewModel model)
        {
            #region Model State Validation 

            if (model.DiscountPercentage <= 0 || model.DiscountPercentage > 100)
            {
                TempData[ErrorMessage] = "بازه ی کدتخفیف پذیرفته نمی باشد.";
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "عملیات باشکست مواچه شده است.";
                return View(model);
            }

            #endregion

            #region Add Discount To The Data Base 

            var res = await _discountCodeService.AddDiscountCodeAdminSide(model);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfDiscountCodes));
            }

            #endregion

            TempData[ErrorMessage] = "عملیات باشکست مواچه شده است.";
            return View(model);
        }

        #endregion

        #region Edit Discount Code

        [HttpGet]
        public async Task<IActionResult> EditDiscountCode(int id)
        {
            #region Get Discount By Id 

            var model = await _discountCodeService.FillEditDiscoutCodeAdminSideViewModel(id);
            if (model == null) return NotFound();

            #endregion

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDiscountCode(EditDiscoutCodeAdminSideViewModel model)
        {
            #region Model State Validation 

            if (model.DiscountPercentage <= 0 || model.DiscountPercentage > 100)
            {
                TempData[ErrorMessage] = "بازه ی کدتخفیف پذیرفته نمی باشد.";
                return View(await _discountCodeService.FillEditDiscoutCodeAdminSideViewModel(model.Id));
            }

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "عملیات باشکست مواچه شده است.";
                return View(await _discountCodeService.FillEditDiscoutCodeAdminSideViewModel(model.Id));
            }

            #endregion

            #region Update Discount Code 

            var res = await _discountCodeService.EditDiscountCode(model);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfDiscountCodes));
            }

            #endregion

            TempData[ErrorMessage] = "عملیات باشکست مواچه شده است.";
            return View(await _discountCodeService.FillEditDiscoutCodeAdminSideViewModel(model.Id));
        }

        #endregion

        #region Delete Discount Code 

        [HttpGet]
        public async Task<IActionResult> DeleteDiscountCode(int id)
        {
            #region Delete Discount Code

            var res = await _discountCodeService.DeleteDiscountCode(id);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfDiscountCodes));
            }

            #endregion

            TempData[ErrorMessage] = "عملیات باشکست مواچه شده است.";
            return RedirectToAction(nameof(ListOfDiscountCodes));
        }

        #endregion

        #endregion

        #region Discount Code Selected From User

        #region List Of Discount Selected From User

        [HttpGet]
        public async Task<IActionResult> FillListOfDiscountSelectedFromUserAdminSideViewModel(int id)
        {
            return View(await _discountCodeService.FillListOfDiscountSelectedFromUserAdminSideViewModel(id));
        }

        #endregion

        #endregion
    }
}
