using Application.Extensions;
using Application.Interfaces;
using Domain.Models.Comment;
using Domain.ViewModels.SiteSide.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParsaWorkShop.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.Controllers
{
    public class ProductsController : SiteBaseController
    {
        #region Ctor

        private IUserService _user;
        private IProductService _product;
        private ICommentService _comment;
        private readonly IFavoriteProductsService _favoriteProductsService;

        public ProductsController(IUserService user, IProductService product, ICommentService comment
                                    , IFavoriteProductsService favoriteProductsService)
        {
            _user = user;
            _product = product;
            _comment = comment;
            _favoriteProductsService = favoriteProductsService;
        }

        #endregion

        #region Index

        public IActionResult Index(int? Categroyid, int pageId = 1, string filter = "", string orderByType = "date")
        {
            ViewBag.Groups = _product.GetAllProductCategories();
            ViewBag.pageId = pageId;
            ViewBag.Categroyid = Categroyid;
            ViewBag.Filter = filter;

            return View(_product.GetProductsForShowInHomePage(Categroyid, pageId, filter, 15, orderByType));
        }

        #endregion

        #region List Of Products

        public async Task<IActionResult> ListOfProducts(ListOfProductsViewModel model)
        {
            return View();
        }

        #endregion

        #region Single Page Products

        [HttpGet("SinglePageProducts/{id}/{ProductTitle}")]
        public async Task<IActionResult> SinglePageProducts(int? id , string ProductTitle)
        {
            if (id == null)
            {
                return NotFound();
            }

            #region Favorite Product

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.IsFavoriteProduct = await _favoriteProductsService.IsUserSelectedThisProductForHisFavoriteProducts(id.Value , User.GetUserId());
            }

            #endregion

            return View(await _product.FillProductDetailSiteSideViewModel((int)id));
        }

        #endregion

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

        #region Add Or Remove Product To The Favorite

        [Authorize, HttpGet]
        public async Task<IActionResult> AddOrRemoveProductToTheFavorite(int productId)
        {
            var res = await _favoriteProductsService.AddorRemoveProductFromFavorite(productId , User.GetUserId());
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(SinglePageProducts) , new { id = productId});
            }

            TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
            return RedirectToAction(nameof(SinglePageProducts), new { id = productId });
        }

        #endregion
    }
}
