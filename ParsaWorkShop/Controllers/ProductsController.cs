using Application.Convertors;
using Application.Extensions;
using Application.Interfaces;
using Domain.Models.Comment;
using Domain.ViewModels.SiteSide.Blog;
using Domain.ViewModels.SiteSide.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ParsaWorkShop.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private readonly IOrderService _orderService;

        public ProductsController(IUserService user, IProductService product, ICommentService comment
                                    , IFavoriteProductsService favoriteProductsService, IOrderService orderService)
        {
            _user = user;
            _product = product;
            _comment = comment;
            _favoriteProductsService = favoriteProductsService;
            _orderService = orderService;
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

        [HttpGet]
        public async Task<IActionResult> ListOfProducts(ListOfProductsViewModel model, int? deletedCategory, int? deletedColor, int? deletedSize)
        {
            #region Model Binding

            if (deletedCategory.HasValue)
            {
                if (model.CategoriesId.Contains(deletedCategory.Value))
                {
                    model.CategoriesId.Remove(deletedCategory.Value);
                }
            }

            if (deletedColor.HasValue)
            {
                if (model.ColorsId.Contains(deletedColor.Value))
                {
                    model.ColorsId.Remove(deletedColor.Value);
                }
            }

            if (deletedSize.HasValue)
            {
                if (model.SizesId.Contains(deletedSize.Value))
                {
                    model.SizesId.Remove(deletedSize.Value);
                }
            }

            #endregion

            #region Model Datas

            ViewData["Categories"] = await _product.ListOfCategoriesForShowInListOfProducts();
            ViewData["Colors"] = await _product.ListOfColorsForShowInListOfProducts();
            ViewData["Sizes"] = await _product.ListOfSizesForShowInListOfProducts();

            ViewData["MaxPrice"] = await _product.GetMaximumPricesOfProducts();
            ViewData["MinPrice"] = await _product.GetMinimumPricesOfProducts();

            if (User.Identity.IsAuthenticated)
            {
                ViewData["FavoriteProducts"] = await _product.ListOFUserFavoriteProductsIds(User.GetUserId());
            }

            #endregion

            var retunModel = await _product.FilterProducts(model);

            return View(retunModel);
        }

        #endregion

        #region Single Page Products

        [HttpGet("SinglePageProducts/{id}/{ProductTitle}")]
        public async Task<IActionResult> SinglePageProducts(int? id, string ProductTitle)
        {
            if (id == null)
            {
                return NotFound();
            }

            #region Favorite Product

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.IsFavoriteProduct = await _favoriteProductsService.IsUserSelectedThisProductForHisFavoriteProducts(id.Value, User.GetUserId());
            }

            #endregion

            #region Check That Is Exist Any Shop Cart With This Product 

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.productOrderDetail = await _orderService.CheckThatIsExistAnyCurrentOrderDetailByThisProductIdAndUserId(User.GetUserId(), id.Value);
            }

            #endregion

            return View(await _product.FillProductDetailSiteSideViewModel((int)id));
        }

        #endregion

        #region Add Comment For Blogs

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> AddCommentForProducts(AddCommentForBlogsSiteSideViewModel model)
        {
            await _comment.AddCommmentForProduct(model, User.GetUserId());

            var product = await _product.GetProductNameByProductId(model.BlogId.Value);

            return RedirectToAction(nameof(SinglePageProducts), new { id = model.BlogId, ProductTitle = product.FixTextForUrl() });
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
            _comment.AddComment(comment, _user.GetUserIdByUserName(User.Identity.Name), 1);

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
            var res = await _favoriteProductsService.AddorRemoveProductFromFavorite(productId, User.GetUserId());
            if (res)
            {
                TempData[SuccessMessage] = "Success";
                return RedirectToAction(nameof(SinglePageProducts), new { id = productId, ProductTitle = await _product.GetProductTitleWithProductId(productId) });
            }

            TempData[ErrorMessage] = "Faild";
            return RedirectToAction(nameof(SinglePageProducts), new { id = productId, ProductTitle = await _product.GetProductTitleWithProductId(productId) });
        }

        #endregion
    }
}
