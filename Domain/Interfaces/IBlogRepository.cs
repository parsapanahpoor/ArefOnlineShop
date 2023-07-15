using Domain.Models.Blog;
using Domain.ViewModels.SiteSide.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBlogRepository
    {

        #region BlogCategory

        BlogCategory GetBlogCategoryById(int id);
        List<BlogCategory> GetAllBlogCategories();
        void AddBlogCategory(BlogCategory blogCategory);
        void UpdateBlogCategroy(BlogCategory blogCategory);
        void Savechanges();

        #endregion


        #region Videos
        List<Video> GetAllVideos();
        List<Video> GetAllDeletedVideos();
        void AddVideo(Video video);
        void UpdateVideo(Video video);
        void AddCategoryToVideo(VideoSelectedCategory video);
        List<VideoSelectedCategory> GetAllVideoSelectedCategories();
        Video GetVideoById(int VideoId);
        void EditeVideoSelectedCategory(int videoid);
        List<Video> GetLastestVideoIndexPageUnder4();
        List<Video> GetLastestVideoIndexPageUpper4();
        int VideotCount();
        #endregion


        #region Blogs

        List<Blog> GetAllBlogs();
        void AddBlog(Blog blog);
        void AddCategoryToBlog(BlogSelectedCategory blog);
        Blog GetBlogById(int blogid);
        List<BlogSelectedCategory> GetAllBlogSelectedCategory();
        void EditBlogSelectedCategory(int BlogId);
        string GetUserNameByBlog(int blogid);
        void UpdateBlog(Blog blog);
        List<Blog> GetAllDeletedBlogs();
        List<Blog> GetLastestBlogIndexPageUnder4();
        List<Blog> GetLastestBlogIndexPageUpper4();
        int BlogtCount();

        #endregion


        #region HomPage

        IQueryable<Blog> SearchForBlog(string Filter);
        IQueryable<Blog> GetAllBlogsForIQueryable();
        IQueryable<Blog> GetBlogsByCategoriID(int? Categroyid);
        IQueryable<Video> SearchForVideo(string Filter);
        IQueryable<Video> GetAllVideosForIQueryable();
        IQueryable<Video> GetVideoByCategoriID(int? Categroyid);
        List<Blog> GetLastestBlogs();
        List<Video> GetLastestVideos();

        #endregion

        #region Site Side 

        Task<ListOfBlogsSiteSideViewModel> FillListOfBlogsSiteSideViewModel(ListOfBlogsSiteSideViewModel filter);

        //Fill Blog Single Page Site Side View Model
        Task<BlogSinglePageSiteSideViewModel> FillBlogSinglePageSiteSideViewModel(int blogId);

        #endregion
    }
}
