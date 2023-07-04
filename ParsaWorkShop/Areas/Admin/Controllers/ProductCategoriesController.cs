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

    public class ProductCategoriesController : Controller
    {
        #region Ctor

        private IProductService _product;

        public ProductCategoriesController(IProductService product)
        {
            _product = product;
        }

        #endregion

        #region Index

        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false)
        {
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;

            return View(_product.GetAllProductCategories());
        }


        #endregion

        #region Create 

        public IActionResult Create(int? id)
        {
            return View(new ProductCategories()
            {
                ParentId = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductCategoryId,CategoryTitle,IsDelete,ParentId")] ProductCategories productCategories ,  IFormFile? imgBlogUp)
        {
            if (ModelState.IsValid)
            {
                _product.AddProductCategories(productCategories , imgBlogUp);
                return Redirect("/Admin/ProductCategories/Index?Create=true");
            }

            return View(productCategories);
        }

        #endregion

        #region Edit

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategories = _product.GetProductCatgeoriesById((int)id);
            if (productCategories == null)
            {
                return NotFound();
            }
            return View(productCategories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductCategoryId,CategoryTitle,IsDelete,ParentId")] ProductCategories productCategories, IFormFile? imgBlogUp)
        {
            if (id != productCategories.ProductCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _product.UpdateProductCategories(productCategories, id , imgBlogUp);

                return Redirect("/Admin/ProductCategories/Index?Edit=true");
            }

            return View(productCategories);
        }


        #endregion

        #region Delete

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategories = _product.GetProductCatgeoriesById((int)id);
            if (productCategories == null)
            {
                return NotFound();
            }

            return View(productCategories);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _product.DeleteProductCategories((int)id);

            return Redirect("/Admin/ProductCategories/Index?Delete=true");
        }

        #endregion
    }
}
