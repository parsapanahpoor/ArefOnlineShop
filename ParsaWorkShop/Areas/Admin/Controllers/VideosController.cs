using Application.Interfaces;
using Application.Security;
using Domain.Models.Blog;
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
    [PermissionChecker(1)]

    public class VideosController : Controller
    {
        #region Ctor

        private IBlogService _blog;
        private IUserService _user;

        public VideosController(IBlogService blog, IUserService user)
        {
            _blog = blog;
            _user = user;
        }

        #endregion

        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false)
        {
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;

            return View(_blog.GetAllVideos());
        }

        public IActionResult DeletedVideos()
        {
            var blog = _blog.GetAllDeletedVideos();
            return View(blog);
        }

        public IActionResult Details(int? id, bool Delete = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["BlogsCategories"] = _blog.GetAllBlogCategories();
            var video = _blog.GetVideoById((int)id);
            if (video == null)
            {
                return NotFound();
            }

            if (Delete == true)
            {
                ViewData["Delete"] = true;
            }

            return View(video);
        }

        public IActionResult Create()
        {
            ViewData["BlogsCategories"] = _blog.GetAllBlogCategories();
            return View();
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("VideoId,UserId,VideoTitle,ShortDescription,LongDescription,VideoImageName,DemoFileName,Tags,CreateDate,IsActive,IsAparat,IsDelete,AparatFileName")] Video video, IFormFile imgBlogUp, IFormFile demoUp, List<int> SelectedCategory)
        {
            if (ModelState.IsValid)
            {
                var user = _user.GetUserByUserName(User.Identity.Name);

                var videoid = _blog.AddVideo(video, imgBlogUp, demoUp, user);
                _blog.AddCategoryToVideo(SelectedCategory, videoid);

                return Redirect("/Admin/Videos/Index?Create=true");
            }
            ViewData["BlogsCategories"] = _blog.GetAllBlogCategories();
            return View(video);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["BlogsCategories"] = _blog.GetAllBlogCategories();

            var video = _blog.GetVideoById((int)id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("VideoId,UserId,VideoTitle,ShortDescription,LongDescription,VideoImageName,DemoFileName,Tags,CreateDate,IsActive,IsAparat,IsDelete ,AparatFileName")] Video video, IFormFile imgBlogUp, IFormFile demoUp, List<int> SelectedCategory)
        {
            if (id != video.VideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var blogid = _blog.UpdateVideo(video, imgBlogUp, demoUp);
                _blog.EditeVideoSelectedCategory(SelectedCategory, blogid);

                return Redirect("/Admin/Videos/Index?Edit=true");
            }
            ViewData["BlogsCategories"] = _blog.GetAllBlogCategories();

            return View(video);
        }

        public IActionResult Delete(int id)
        {
            var blog = _blog.GetVideoById(id);
            _blog.DeleteVideos(blog);
            return Redirect("/Admin/Videos/Index?Delete=true");
        }

        public IActionResult LockUser(int videoid, int id)
        {
            var blog = _blog.GetVideoById(videoid);

            if (id == 1)
            {
                blog.IsActive = false;
            }
            if (id == 2)
            {
                blog.IsActive = true;
            }
            _blog.UpdateBlogForLock(blog);
            return RedirectToAction(nameof(Index));
        }
    }
}
