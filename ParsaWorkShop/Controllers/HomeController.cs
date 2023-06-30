using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParsaWorkShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.Controllers
{
    public class HomeController : Controller
    {
        #region Ctor
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        #endregion

        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Client, NoStore = false)]
        public IActionResult Index()
        {
            return View();
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
