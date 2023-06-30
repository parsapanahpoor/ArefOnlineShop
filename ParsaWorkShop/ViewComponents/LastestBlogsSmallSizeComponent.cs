using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.ViewComponents
{
    public class LastestBlogsSmallSizeComponent : ViewComponent
    {
        private IBlogService _blog;
        public LastestBlogsSmallSizeComponent(IBlogService blog)
        {
            _blog = blog;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LastestBlogsSmallSizeComponent", _blog.GetLastestBlogs()));
        }
    }
}
