#region Usings

using Application.Extensions;
using Application.Interfaces;
using Domain.Models.Blog;
using Domain.Models.Comment;
using Domain.ViewModels.SiteSide.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

#endregion

namespace ParsaWorkShop.Controllers
{
    public class BlogController : Controller
    {
        #region Ctor

        private IBlogService _blog;
        private IUserService _user;
        private ICommentService _comment;
        public BlogController(IBlogService blog, IUserService user, ICommentService comment)
        {
            _blog = blog;
            _user = user;
            _comment = comment;
        }

        #endregion

        #region Blogs

        public IActionResult Index(int? Categroyid, int pageId = 1, string filter = "")
        {
            ViewBag.Groups = _blog.GetAllBlogCategories();
            ViewBag.pageId = pageId;
            ViewBag.Categroyid = Categroyid;
            ViewBag.Filter = filter;

            return View(_blog.GetBlogsForShowInHomePage(Categroyid, pageId, filter, 9));
        }

        #region Single Blogs Page

        public async Task<IActionResult> SingleBlogsPage(int id)
        {
            #region Fill Model

            var model = await _blog.FillBlogSinglePageSiteSideViewModel(id);
            if (model == null) return NotFound(); 

            #endregion

            return View(model);
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
            _comment.AddComment(comment , _user.GetUserIdByUserName(User.Identity.Name) , 2);

            return View("ShowComment", _comment.GetBlogComment((int)comment.BlogId));
        }

        public IActionResult ShowComment(int id, int pageId = 1)
        {
            return View(_comment.GetBlogComment(id, pageId));
        }
        #endregion

        #region List Of Blogs  

        public async Task<IActionResult> ListOfBlogs(ListOfBlogsSiteSideViewModel model)
        {
            return View(await _blog.FillListOfBlogsSiteSideViewModel(model));
        }

        #endregion

        #region Add Comment For Blogs

        [HttpPost , ValidateAntiForgeryToken , Authorize]
        public async Task<IActionResult> AddCommentForBlogs(AddCommentForBlogsSiteSideViewModel model )
        {
            await _comment.AddCommmentForBlog(model , User.GetUserId());

            return RedirectToAction(nameof(SingleBlogsPage) , new { id = model.BlogId });
        }

        #endregion

        #endregion

        #region Videos

        public IActionResult Video(int? Categroyid, int pageId = 1, string filter = "")
        {
            ViewBag.Groups = _blog.GetAllBlogCategories();
            ViewBag.pageId = pageId;
            ViewBag.Categroyid = Categroyid;
            ViewBag.Filter = filter;

            return View(_blog.GetVideosForShowInHomePage(Categroyid, pageId, filter, 12));
        }
        public IActionResult SingleVideoPage(int id)
        {
            Video blog = _blog.GetVideoById(id);

            return View(blog);
        }

        #region VideoComments

        public IActionResult CreateVideoComments(Comment comment)
        {
   
            _comment.AddComment(comment , _user.GetUserIdByUserName(User.Identity.Name) , 3);

            return View("ShowCommentVideo", _comment.GetVideoComment((int)comment.VideoId));
        }

        public IActionResult ShowCommentVideo(int id, int pageId = 1)
        {
            return View(_comment.GetVideoComment(id, pageId));
        }

        #endregion

        #endregion
    }
}
