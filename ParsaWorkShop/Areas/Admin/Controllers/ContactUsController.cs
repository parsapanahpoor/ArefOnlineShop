using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    public class ContactUsController : AdminBaseController
    {
        #region Ctor

        private readonly IUsersCommentAboutSiteService _comment;

        public ContactUsController(IUsersCommentAboutSiteService comment)
        {
            _comment = comment;
        }

        #endregion

        #region List Of Comments

        public async Task<IActionResult> ListOfContactUs()
        {
            return View(await _comment.ListOfContactUsRequests());
        }

        #endregion

        #region Users Comments About Site 

        public async Task<IActionResult> GetContactUsWithId(int id)
        {
            return View(await _comment.GetContactUsWithId(id));
        }

        #endregion
    }
}
