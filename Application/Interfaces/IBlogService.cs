using Domain.Models.Blog;
using Domain.Models.Users;
using Domain.ViewModels.SiteSide.Blog;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBlogService
    {

        #region Categories

        BlogCategory GetBlogCategoryById(int id);
        List<BlogCategory> GetAllBlogCategories();
        void AddBlogCategory(BlogCategory blogCategory);
        void UpdateBlogCategroy(BlogCategory blogCategory);
        void DeleteBlogCategory(int id);

        #endregion

        #region Videos
        List<Video> GetAllVideos();
        List<Video> GetAllDeletedVideos();
        int AddVideo(Video video, IFormFile imgBlogUp, IFormFile demoUp, User user);
        int UpdateVideo(Video video, IFormFile imgBlogUp, IFormFile demoUp);
        void AddCategoryToVideo(List<int> Categories, int videoid);
        List<VideoSelectedCategory> GetAllVideoSelectedCategories();
        Video GetVideoById(int VideoId);
        void EditeVideoSelectedCategory(List<int> Categories, int videoid);
        void DeleteVideos(Video video);
        void UpdateBlogForLock(Video video);
        #endregion

        #region Blog
        List<Blog> GetAllBlogs();
        int AddBlog(Blog blog, IFormFile imgBlogUp, User user);
        void AddCategoryToBlog(List<int> Categories, int BlogId);
        int UpdateBlog(Blog blog, IFormFile imgBlogUp);
        void UpdateBlogs(Blog blog);
        Blog GetBlogById(int blogid);
        List<BlogSelectedCategory> GetAllBlogSelectedCategory();
        void EditBlogSelectedCategory(List<int> Categories, int BlogId);
        string GetUserNameByBlog(int blogid);
        void DeleteBlog(Blog blog);
        List<Blog> GetAllDeletedBlogs();
        #endregion

        #region HomePage

        Tuple<List<Blog>, int> GetBlogsForShowInHomePage(int? Categroyid, int pageId = 1, string filter = "", int take = 0);
        Tuple<List<Video>, int> GetVideosForShowInHomePage(int? Categroyid, int pageId = 1, string filter = "", int take = 0);
        List<Blog> GetLastestBlogs();
        List<Video> GetLastestVideos();

        #endregion

        #region Site Side 

        Task<ListOfBlogsSiteSideViewModel> FillListOfBlogsSiteSideViewModel(ListOfBlogsSiteSideViewModel filter);

        #endregion
    }
}
