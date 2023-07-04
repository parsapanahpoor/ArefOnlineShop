using Microsoft.AspNetCore.Mvc;

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
        {
            public static string SuccessMessage = "SuccessMessage";
            public static string ErrorMessage = "ErrorMessage";
            public static string InfoMessage = "InfoMessage";
            public static string WarningMessage = "WarningMessage";
        }
}
