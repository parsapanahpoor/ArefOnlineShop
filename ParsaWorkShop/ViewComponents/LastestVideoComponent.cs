using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.ViewComponents
{
    public class LastestVideoComponent : ViewComponent
    {
        private IBlogService _blog;

        public LastestVideoComponent(IBlogService blog)
        {
            _blog = blog;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LastestVideoComponent", _blog.GetLastestVideos()));
        }
    }
}
