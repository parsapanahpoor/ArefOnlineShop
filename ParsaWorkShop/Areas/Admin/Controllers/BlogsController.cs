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
    [PermissionChecker(9)]

    public class BlogsController : Controller
    {
        #region Ctor

        private IBlogService _blog;
        private IUserService _userservice;
        private ICommentService _comment;

        public BlogsController(IBlogService blog, IUserService userService, ICommentService comment)
        {
            _blog = blog;
            _userservice = userService;
            _comment = comment;
        }

        #endregion

        #region VideoComments

        public IActionResult ShowBlogCommentsInAdmin(bool Delete = false)
        {
            ViewData["Delete"] = Delete;

            return View(_comment.GetAllVideosComments());
        }

        public IActionResult LockVideoComment(int commentid, int id)
        {
            var comment = _comment.GetCommentById(commentid);

            if (id == 1)
            {
                comment.IsAdminRead = false;
            }
            if (id == 2)
            {
                comment.IsAdminRead = true;
            }
            _comment.UpdateComment(comment);

            return RedirectToAction(nameof(ShowBlogCommentsInAdmin));
        }

        public IActionResult CommenVideotDetails(int commenid)
        {
            var comment = _comment.GetCommentById(commenid);

            return View(comment);
        }

        public IActionResult DeleteVideoComment(int id)
        {
            _comment.DeleteComment(id);

            return Redirect("/Admin/Blogs/ShowBlogCommentsInAdmin?Delete=true");
        }

        public IActionResult DeletedVideoComments()
        {
            return View(_comment.DeletedVideoComments());
        }

        public IActionResult ShowVideoComments(int blogid)
        {
            var blog = _blog.GetVideoById(blogid);
            ViewData["BlogImageName"] = blog.VideoImageName;
            return View(_comment.GetCommentByVideoId(blogid));
        }

        #endregion

        #region BlogComments

        public IActionResult ShowBlogComments(bool Delete = false)
        {
            ViewData["Delete"] = Delete;

            return View(_comment.GetAllBlogsComments());
        }

        public IActionResult LockComment(int commentid, int id)
        {
            var comment = _comment.GetCommentById(commentid);

            if (id == 1)
            {
                comment.IsAdminRead = false;
            }
            if (id == 2)
            {
                comment.IsAdminRead = true;
            }
            _comment.UpdateComment(comment);
            return RedirectToAction(nameof(ShowBlogComments));
        }

        public IActionResult CommentDetails(int commenid)
        {
            var comment = _comment.GetCommentById(commenid);

            return View(comment);
        }

        public IActionResult DeleteComment(int id)
        {
            _comment.DeleteComment(id);

            return Redirect("/Admin/Blogs/ShowBlogComments?Delete=true");

        }

        public IActionResult DeletedComments()
        {
            return View(_comment.DeletedComments());
        }

        public IActionResult ShowProductComments(int blogid)
        {
            var blog = _blog.GetBlogById(blogid);
            ViewData["BlogImageName"] = blog.BlogImageName;
            return View(_comment.GetCommentByBlogId(blogid));
        }

        #endregion

        #region Blog

        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false)
        {
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;

            return View(_blog.GetAllBlogs());
        }

        public IActionResult DeletedUsers()
        {
            var blog = _blog.GetAllDeletedBlogs();
            return View(blog);
        }

        public IActionResult Details(int? id, bool Delete = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = _blog.GetBlogById((int)id);
            ViewData["UserName"] = _blog.GetUserNameByBlog((int)id);
            ViewData["BlogsCategories"] = _blog.GetAllBlogCategories();
            ViewData["BlogSelectedCategory"] = _blog.GetAllBlogSelectedCategory();
            if (blog == null)
            {
                return NotFound();
            }
            if (Delete == true)
            {
                ViewData["Delete"] = true;
            }

            return View(blog);
        }

        public IActionResult Create()
        {
            ViewData["BlogsCategories"] = _blog.GetAllBlogCategories();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BlogId,UserId,BlogTitle,ShortDescription,LongDescription,BlogImageName,Tags,CreateDate,IsActive,IsDelete")] Blog blog, IFormFile imgBlogUp, List<int> SelectedCategory)
        {
            if (ModelState.IsValid)
            {
                var user = _userservice.GetUserByUserName(User.Identity.Name);
                var blogid = _blog.AddBlog(blog, imgBlogUp, user);
                _blog.AddCategoryToBlog(SelectedCategory, blogid);

                return Redirect("/Admin/Blogs/Index?Create=true");
            }
            ViewData["BlogsCategories"] = _blog.GetAllBlogCategories();

            return View(blog);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["BlogsCategories"] = _blog.GetAllBlogCategories();
            ViewData["BlogSelectedCategory"] = _blog.GetAllBlogSelectedCategory();

            var blog = _blog.GetBlogById((int)id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BlogId,UserId,BlogTitle,ShortDescription,LongDescription,BlogImageName,Tags,CreateDate,IsActive,IsDelete")] Blog blog, IFormFile imgBlogUp, List<int> SelectedCategory)
        {
            if (id != blog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var blogid = _blog.UpdateBlog(blog, imgBlogUp);
                _blog.EditBlogSelectedCategory(SelectedCategory, blogid);

                return Redirect("/Admin/Blogs/Index?Edit=true");
            }
            ViewData["BlogsCategories"] = _blog.GetAllBlogCategories();

            return View(blog);
        }

        public IActionResult Delete(int id)
        {
            var blog = _blog.GetBlogById(id);
            _blog.DeleteBlog(blog);
            return Redirect("/Admin/Blogs/Index?Delete=true");
        }

        public IActionResult LockUser(int blogid, int id)
        {
            var blog = _blog.GetBlogById(blogid);

            if (id == 1)
            {
                blog.IsActive = false;
            }
            if (id == 2)
            {
                blog.IsActive = true;
            }

            _blog.UpdateBlogs(blog);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
