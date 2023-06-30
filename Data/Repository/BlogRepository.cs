using Data.Context;
using Domain.Interfaces;
using Domain.Models.Blog;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private ParsaWorkShopContext _context;

        public BlogRepository(ParsaWorkShopContext context)
        {
            _context = context;
        }

        public void AddBlog(Blog blog)
        {
            _context.Blog.Add(blog);
            Savechanges();
        }

        public void AddBlogCategory(BlogCategory blogCategory)
        {
            _context.BlogCategories.Add(blogCategory);

            Savechanges();
        }

        public void AddCategoryToBlog(BlogSelectedCategory blog)
        {
            _context.BlogSelectedCategories.Add(blog);
            Savechanges();
        }

        public void AddCategoryToVideo(VideoSelectedCategory video)
        {
            _context.VideoSelectedCategory.Add(video);
            Savechanges();
        }

        public void AddVideo(Video video)
        {
            _context.Video.Add(video);

            Savechanges();
        }

        public int BlogtCount()
        {
            return _context.product.Count();
        }

        public void EditBlogSelectedCategory(int BlogId)
        {
            _context.BlogSelectedCategories.Where(p => p.BlogId == BlogId).ToList()
                                                    .ForEach(p => _context.BlogSelectedCategories.Remove(p));
        }

        public void EditeVideoSelectedCategory(int videoid)
        {
            var groups = _context.VideoSelectedCategory.Where(p => p.VideoId == videoid).ToList();

            foreach (var item in groups)
            {
                _context.VideoSelectedCategory.Remove(item);
            }
        }

        public List<BlogCategory> GetAllBlogCategories()
        {
            return _context.BlogCategories.ToList();
        }

        public List<Blog> GetAllBlogs()
        {
            return _context.Blog.Include(p => p.Users).ToList();
        }

        public List<BlogSelectedCategory> GetAllBlogSelectedCategory()
        {
            return _context.BlogSelectedCategories.ToList();
        }

        public IQueryable<Blog> GetAllBlogsForIQueryable()
        {
            return _context.Blog.Include(p => p.Users).Include(p => p.BlogSelectedCategory)
                                 .Where(p => p.IsActive).OrderByDescending(p => p.CreateDate);
        }

        public List<Blog> GetAllDeletedBlogs()
        {
            IQueryable<Blog> result = _context.Blog.Include(p => p.Users)
                            .IgnoreQueryFilters().Where(u => u.IsDelete);

            return result.ToList();
        }

        public List<Video> GetAllDeletedVideos()
        {
            IQueryable<Video> result = _context.Video.Include(p => p.Users)
                            .IgnoreQueryFilters().Where(u => u.IsDelete);

            return result.ToList();
        }

        public List<Video> GetAllVideos()
        {
            return _context.Video.Include(p => p.Users).ToList();
        }

        public List<VideoSelectedCategory> GetAllVideoSelectedCategories()
        {
            return _context.VideoSelectedCategory.ToList();
        }

        public Blog GetBlogById(int blogid)
        {
            return _context.Blog.Include(p => p.Users).FirstOrDefault(p => p.BlogId == blogid);
        }

        public BlogCategory GetBlogCategoryById(int id)
        {
            return _context.BlogCategories.Find(id);
        }

        public IQueryable<Blog> GetBlogsByCategoriID(int? Categroyid)
        {
            return _context.BlogSelectedCategories.Where(p => p.BlogCategoryId == Categroyid).Include(p => p.Blog)
                                       .ThenInclude(p => p.Users).Select(p => p.Blog);                                             
        }

        public List<Blog> GetLastestBlogIndexPageUnder4()
        {
            return _context.Blog.Include(p=>p.Users)
                .OrderByDescending(p => p.CreateDate).ToList();
        }

        public List<Blog> GetLastestBlogIndexPageUpper4()
        {
            return _context.Blog.Include(p=>p.Users)
                          .OrderByDescending(p => p.CreateDate).Take(4).ToList();
        }

        public List<Blog> GetLastestBlogs()
        {
            return _context.Blog.Include(p=>p.Users)
                      .OrderByDescending(p => p.CreateDate).ToList();
        }

        public List<Video> GetLastestVideoIndexPageUnder4()
        {
            return _context.Video.OrderByDescending(p => p.CreateDate).ToList();
        }

        public List<Video> GetLastestVideoIndexPageUpper4()
        {
            return _context.Video.OrderByDescending(p => p.CreateDate).Take(4).ToList();
        }

        public List<Video> GetLastestVideos()
        {
            return _context.Video.OrderByDescending(p => p.CreateDate).ToList();
        }

        public string GetUserNameByBlog(int blogid)
        {
            return _context.Blog.Where(p => p.BlogId == blogid).Include(p => p.Users).Select(p => p.Users.UserName).Single();
        }

        public IQueryable<Video> GetVideoByCategoriID(int? Categroyid)
        {
            return _context.VideoSelectedCategory.Where(p => p.BlogCategoryId == Categroyid).Include(p => p.Video)
                                       .ThenInclude(p => p.Users).Select(p => p.Video);
        }

        public Video GetVideoById(int VideoId)
        {
            return _context.Video.Include(p => p.Users).Include(p => p.VideoSelectedCategory)
                                            .FirstOrDefault(p => p.VideoId == VideoId);
        }

        public void Savechanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<Blog> SearchForBlog(string Filter)
        {
            return _context.Blog.Where(c => c.BlogTitle.Contains(Filter) || c.Tags.Contains(Filter)).Include(p => p.Users);
        }

        public IQueryable<Video> SearchForVideo(string Filter)
        {
            return _context.Video.Where(c => c.VideoTitle.Contains(Filter) || c.Tags.Contains(Filter)).Include(p => p.Users);
        }

        public void UpdateBlog(Blog blog)
        {
            _context.Blog.Update(blog);
            Savechanges();
        }

        public void UpdateBlogCategroy(BlogCategory blogCategory)
        {
            _context.BlogCategories.Update(blogCategory);

            Savechanges();
        }

        public void UpdateVideo(Video video)
        {
            _context.Video.Update(video);

            Savechanges();
        }

        public int VideotCount()
        {
            return _context.Video.Count();
        }

        IQueryable<Video> IBlogRepository.GetAllVideosForIQueryable()
        {
            return _context.Video.Include(p => p.Users).Include(p => p.VideoSelectedCategory)
                                            .Where(p => p.IsActive).OrderByDescending(p => p.CreateDate);
        }
    }
}
