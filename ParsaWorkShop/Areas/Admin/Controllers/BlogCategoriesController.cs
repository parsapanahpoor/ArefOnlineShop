using Application.Interfaces;
using Application.Security;
using Domain.Models.Blog;
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
    [PermissionChecker(3)]

    public class BlogCategoriesController : Controller
    {
        #region Ctor

        private IBlogService _blog;

        public BlogCategoriesController(IBlogService blog)
        {
            _blog = blog;
        }

        #endregion


        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false)
        {
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;
            return View(_blog.GetAllBlogCategories());
        }

        public IActionResult Create(int? id)
        {
            return View(new BlogCategory()
            {
                ParentId = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BlogCategoryId,CategoryTitle,IsDelete,ParentId")] BlogCategory blogCategory)
        {
            if (ModelState.IsValid)
            {
                _blog.AddBlogCategory(blogCategory);
                return Redirect("/Admin/BlogCategories/Index?Create=true");


            }
            return View(blogCategory);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = _blog.GetBlogCategoryById((int)id);
            if (blogCategory == null)
            {
                return NotFound();
            }
            return View(blogCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BlogCategoryId,CategoryTitle,IsDelete,ParentId")] BlogCategory blogCategory)
        {
            if (id != blogCategory.BlogCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _blog.UpdateBlogCategroy(blogCategory);


                return Redirect("/Admin/BlogCategories/Index?Edit=true");
            }
            return View(blogCategory);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = _blog.GetBlogCategoryById((int)id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {


            _blog.DeleteBlogCategory((int)id);


            return Redirect("/Admin/BlogCategories/Index?Delete=true");
        }
    }
}
