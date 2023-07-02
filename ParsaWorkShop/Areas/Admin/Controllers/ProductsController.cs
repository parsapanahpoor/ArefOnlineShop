using Application.Interfaces;
using Application.Security;
using Domain.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    public class ProductsController : Controller
    {
        #region Ctor
        private IProductService _product;
        private IUserService _user;
        private ICommentService _comment;

        public ProductsController(IProductService product, IUserService user , ICommentService comment)
        {
            _product = product;
            _user = user;
            _comment = comment;
        }
        #endregion


        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false)
        {
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;

            return View(_product.GetAllProducts());
        }

        public IActionResult DeletedProducts()
        {
            var product = _product.GetAllDeletedProducts();
            return View(product);
        }

        public IActionResult Create()
        {
            ViewData["ProductCategories"] = _product.GetAllProductCategories();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductID,UserId,ProductTitle,ShortDescription,LongDescription,ProductImageName,OfferPercent,IsInOffer,ProductCount,Price,Tags,CreateDate,IsActive,IsDelete")] Product product, IFormFile imgProductUp, List<int> SelectedCategory)
        {
            if (ModelState.IsValid)
            {
                #region Check Category 

                if (SelectedCategory == null || !SelectedCategory.Any())
                {
                    ViewData["ProductCategories"] = _product.GetAllProductCategories();

                    return View(product);
                }

                #endregion

                var user = _user.GetUserByUserName(User.Identity.Name);
                var ProductID = _product.AddProduct(product, imgProductUp, user);
                _product.AddCategoryToProduct(SelectedCategory, ProductID);

                return Redirect("/Admin/Products/Index?Create=true");
            }
            ViewData["ProductCategories"] = _product.GetAllProductCategories();

            return View(product);
        }

        public IActionResult Edit(int? id, bool Detail = false, bool Delete = false)
        {
            ViewBag.Detail = Detail;
            ViewBag.Delete = Delete;

            if (id == null)
            {
                return NotFound();
            }

            var product = _product.GetProductByID((int)id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductCategories"] = _product.GetAllProductCategories();
            ViewData["ProductSelectedCategories"] = _product.GetAllProductSelectedCategories();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductID,UserId,ProductTitle,ShortDescription,LongDescription,ProductImageName,OfferPercent,IsInOffer,ProductCount,Price,Tags,CreateDate,IsActive,IsDelete,OldPrice")] Product product, IFormFile imgProductUp, List<int> SelectedCategory)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var productid = _product.UpdateProduct(product, imgProductUp);
                _product.EditProductSelectedCategory(SelectedCategory, productid);

                return Redirect("/Admin/Products/Index?Edit=true");
            }
            ViewData["ProductCategories"] = _product.GetAllProductCategories();
            ViewData["ProductSelectedCategories"] = _product.GetAllProductSelectedCategories();

            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _product.GetProductByID(id);
            _product.DeleteProduct(product);
            return Redirect("/Admin/Products/Index?Delete=true");
        }

        public IActionResult LockProduct(int productid, int id)
        {
            var product = _product.GetProductByID(productid);

            if (id == 1)
            {
                product.IsActive = false;

            }
            if (id == 2)
            {
                product.IsActive = true;
            }
            _product.UpdateProductForLock(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProductFeaturs(int id)
        {
            ViewBag.Features = _product.GetProductFeaturs(id);

            return View(new ProductFeature()
            {
                ProductID = id
            });
        }

        [HttpPost]
        public IActionResult ProductFeaturs(ProductFeature productFeature)
        {
            if (ModelState.IsValid)
            {
                _product.AddFeatureToProduct(productFeature);
            }

            return RedirectToAction("ProductFeaturs", new { id = productFeature.ProductID });
        }

        public void DeleteFeature(int id)
        {
            var feature = _product.GetFeatureById(id);
            _product.DeleteProductFeature(feature);
        }

        public ActionResult Gallery(int id)
        {
            ViewBag.Galleries = _product.GetGalleryById(id);
            return View(new ProductGallery()
            {
                ProductID = id
            });
        }
        [HttpPost]
        public ActionResult Gallery(ProductGallery galleries, IFormFile imgUp)
        {
            if (ModelState.IsValid)
            {
                _product.AddImageToGalleryProduct(galleries, imgUp);
            }

            return RedirectToAction("Gallery", new { id = galleries.ProductID });
        }

        public IActionResult DeleteGallery(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductGallery product = _product.GetProductGalleryByID((int)id);
            _product.DeleteProductGallery(product);

            return RedirectToAction("Gallery", new { id = product.ProductID });
        }


        #region ProductsComments

        public IActionResult ShowProductsComments(bool Delete = false)
        {
            ViewData["Delete"] = Delete;

            return View(_comment.GetAllProductsComments());
        }

        public IActionResult CommentDetails(int commenid)
        {
            var comment = _comment.GetCommentById(commenid);

            return View(comment);
        }

        public IActionResult DeleteComment(int id)
        {
            _comment.DeleteComment(id);

            return Redirect("/Admin/Products/ShowProductsComments?Delete=true");
        }

        public IActionResult DeletedComments()
        {
            return View(_comment.DeletedComments());
        }

        public IActionResult ShowProductComments(int productid)
        {
            var product = _product.GetProductByID(productid);
            ViewData["ProductImageName"] = product.ProductImageName;
            return View(_comment.GetCommentByBlogId(productid));
        }

        #endregion


        #region Offer

        public IActionResult ListOfProductsInOffer(bool Add = false , bool Delete = false)
        {
            if (Add == true)
            {
                ViewBag.Add = true;
            }
            if (Delete == true)
            {
                ViewBag.Delete = true;
            }

            return View(_product.GetAllProductsInOffer());
        }

        public IActionResult ListOffProductsNotInOffer()
        {
            return View(_product.GetAllProductsNotInOffer());
        }

        public IActionResult AddProductToTheOffer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = _product.GetProductByID((int)id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductCategories"] = _product.GetAllProductCategories();
            ViewData["ProductSelectedCategories"] = _product.GetAllProductSelectedCategories();

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProductToTheOffer(int id, [Bind("ProductID,UserId,ProductTitle,ShortDescription,LongDescription,ProductImageName,OfferPercent,IsInOffer,ProductCount,Price,Tags,CreateDate,IsActive,IsDelete,OldPrice")] Product product, IFormFile imgProductUp, List<int> SelectedCategory)
        {
            if (ModelState.IsValid)
            {
                product.IsInOffer = true;
                var productid = _product.UpdateProduct(product, imgProductUp);
                _product.EditProductSelectedCategory(SelectedCategory, productid);

                return Redirect("/Admin/Products/ListOfProductsInOffer?Add=true");
            }

            ViewData["ProductCategories"] = _product.GetAllProductCategories();
            ViewData["ProductSelectedCategories"] = _product.GetAllProductSelectedCategories();
            return View(product);
        }

        public IActionResult DeleteProductFromOffer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = _product.GetProductByID((int)id);
            _product.DeleteProductFromOffer(product);

            return Redirect("/Admin/Products/ListOfProductsInOffer?Delete=true");
        }
        #endregion

    }
}
