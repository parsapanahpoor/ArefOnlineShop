﻿using Application.Interfaces;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ParsaWorkShop.ViewComponents
{
    public class SiteSideBarViewComponent : ViewComponent
    {
        #region Ctor

        public IPermissionService _permissionService;
        private readonly IProductService _productService;

        public SiteSideBarViewComponent(IPermissionService permissionService, IProductService productService)
        {
            _permissionService = permissionService;
            _productService = productService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            #region Categories

            var model = await _productService.FillSiteSideBar();

            #endregion


            return View("SiteSideBar" , model);
        }
    }
}
