using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ParsaWorkShop.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class UserPanelBaseController : Controller
    {
        public static string SuccessMessage = "SuccessMessage";
        public static string ErrorMessage = "ErrorMessage";
        public static string InfoMessage = "InfoMessage";
        public static string WarningMessage = "WarningMessage";
    }
}
