#region Usings

using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsaWorkShop.Models;
using ParsaWorkShop.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace ParsaWorkShop.Controllers
{
    public class HomeController : SiteBaseController
    {
        #region Ctor

        private readonly ILogger<HomeController> _logger;
        private readonly ISiteSettingService _siteSettingService;

        public HomeController(ILogger<HomeController> logger , ISiteSettingService siteSettingService)
        {
            _logger = logger;
            _siteSettingService = siteSettingService;
        }

        #endregion

        //[ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<IActionResult> Index()
        {
            #region Fill Model

            var model = await _siteSettingService.FillIndexPageViewModel();

            #endregion

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
