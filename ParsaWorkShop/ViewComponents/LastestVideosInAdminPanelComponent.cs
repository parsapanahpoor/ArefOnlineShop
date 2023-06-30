using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.ViewComponents
{
    public class LastestVideosInAdminPanelComponent : ViewComponent
    {
        private IBlogService _blog;

        public LastestVideosInAdminPanelComponent(IBlogService blog)
        {
            _blog = blog;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LastestVideosInAdminPanelComponent", _blog.GetLastestVideos()));
        }
    }
}
