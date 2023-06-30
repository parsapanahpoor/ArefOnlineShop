using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.ViewComponents
{
    public class LastestVideosSmallSizeComponent : ViewComponent
    {
        private IBlogService _blog;
        public LastestVideosSmallSizeComponent(IBlogService blog)
        {
            _blog = blog;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LastestVideosSmallSizeComponent", _blog.GetLastestVideos()));
        }
    }
}
