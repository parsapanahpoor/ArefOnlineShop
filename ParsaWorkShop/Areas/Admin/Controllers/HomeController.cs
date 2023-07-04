using Application.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    //[PermissionChecker(1)]

    public class HomeController : AdminBaseController
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
