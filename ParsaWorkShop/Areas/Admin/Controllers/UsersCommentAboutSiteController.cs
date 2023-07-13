using Application.Interfaces;
using Application.Security;
using Domain.ViewModels.Admin.UsersCommentAboutSite;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    [PermissionChecker(1)]
    public class UsersCommentAboutSiteController : AdminBaseController
    {
        #region Ctor 

        private readonly IUsersCommentAboutSiteService _usersCommentAboutSite;

        public UsersCommentAboutSiteController(IUsersCommentAboutSiteService usersCommentAboutSite)
        {
            _usersCommentAboutSite = usersCommentAboutSite;
        }

        #endregion

        #region Filter Comments 

        [HttpGet]
        public async Task<IActionResult> ListOfUsersComments()
        {
            return View(await _usersCommentAboutSite.FilterUserCommentAboutSiteAdminSideViewModel());
        }

        #endregion

        #region Create User Comment

        [HttpGet]
        public async Task<IActionResult> CreateUserComment()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUserComment(CreateUsersCommentsAdminSideViewModel model)
        {
            #region Model State Validation 

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "عملیات باشکست مواجه شده است .";
                return View(model);
            }

            #endregion

            #region Add To The Data Base 

            var res = await _usersCommentAboutSite.CreateUserComment(model);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfUsersComments));
            }

            #endregion

            TempData[ErrorMessage] = "عملیات باشکست مواجه شده است .";
            return View(model);
        }

        #endregion

        #region Edit User Comment 

        [HttpGet]
        public async Task<IActionResult> EditUserComment(int id)
        {
            var model = await _usersCommentAboutSite.FillEditUsersCommentsAdminSideViewModel(id);
            if (model == null) return NotFound();

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserComment(EditUsersCommentsAdminSideViewModel model)
        {
            #region Model State Validation 

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "عملیات باشکست مواجه شده است .";
                return View(model);
            }

            #endregion

            #region Update Method

            var res = await _usersCommentAboutSite.EditUserComment(model);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfUsersComments));
            }

            #endregion

            TempData[ErrorMessage] = "عملیات باشکست مواجه شده است .";
            return View(model);
        }

        #endregion

        #region Delete User Comment

        [HttpGet]
        public async Task<IActionResult> DeleteUserComment(int id)
        {
            var res = await _usersCommentAboutSite.DeleteUsersComment(id);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(ListOfUsersComments));
            }


            TempData[ErrorMessage] = "عملیات باشکست مواجه شده است .";
            return RedirectToAction(nameof(ListOfUsersComments));

        }

        #endregion
    }
}
