using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.ViewComponents
{
    public class LastestUserForAdminPageComponent : ViewComponent
    {
        private IUserService _userService;
        public LastestUserForAdminPageComponent(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LastestUserForAdminPageComponent", _userService.GetLastestUserForAdminPage()));
        }
    }
}
