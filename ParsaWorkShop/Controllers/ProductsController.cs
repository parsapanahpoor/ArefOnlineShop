using Application.Interfaces;
using Domain.Models.Comment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.Controllers
{
    public class ProductsController : Controller
    {
        #region Ctor

        private IUserService _user;
        private IProductService _product;
        private ICommentService _comment;

        public ProductsController(IUserService user, IProductService product, ICommentService comment)
        {
            _user = user;
            _product = product;
            _comment = comment;
        }

        #endregion

        public IActionResult Index(int? Categroyid, int pageId = 1, string filter = "", string orderByType = "date")
        {
            ViewBag.Groups = _product.GetAllProductCategories();
            ViewBag.pageId = pageId;
            ViewBag.Categroyid = Categroyid;
            ViewBag.Filter = filter;

            return View(_product.GetProductsForShowInHomePage(Categroyid, pageId, filter, 15 , orderByType));
        }
        public IActionResult SinglePageProducts(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_product.GetProductForShowInSingleProducPage((int)id));
        }

        #region ProductsComments

        [HttpPost]
        public IActionResult CreateCommente(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _comment.AddComment(comment , _user.GetUserIdByUserName(User.Identity.Name) , 1);

            return View("ShowComment", _comment.GetProductComment((int)comment.ProductID));
        }

        public IActionResult ShowComment(int id, int pageId = 1)
        {
            return View(_comment.GetProductComment(id, pageId));
        }
        #endregion

    }
}
