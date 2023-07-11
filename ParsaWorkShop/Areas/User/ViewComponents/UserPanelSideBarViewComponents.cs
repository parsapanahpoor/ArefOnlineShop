using Application.Extensions;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ParsaWorkShop.Areas.User.ViewComponents
{
    public class UserPanelSideBarViewComponents : ViewComponent
    {
        #region Ctor

        private readonly IUserService _userService;

        public UserPanelSideBarViewComponents(IUserService userService)
        {
                _userService = userService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("UserPanelSideBar" , await _userService.GetUserByIdAsync(User.GetUserId()));
        }
    }
}
