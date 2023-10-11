using Data.Context;
using Domain.Interfaces;
using Domain.Models.Blog;
using Domain.Models.Product;
using Domain.ViewModels.SiteSide.Blog;
using Domain.ViewModels.SiteSide.Product;
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
            return _context.Blog.Include(p => p.Users)
                .OrderByDescending(p => p.CreateDate).ToList();
        }

        public List<Blog> GetLastestBlogIndexPageUpper4()
        {
            return _context.Blog.Include(p => p.Users)
                          .OrderByDescending(p => p.CreateDate).Take(4).ToList();
        }

        public List<Blog> GetLastestBlogs()
        {
            return _context.Blog.Include(p => p.Users)
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

        #region Site Side

        public async Task<ListOfBlogsSiteSideViewModel> FillListOfBlogsSiteSideViewModel(ListOfBlogsSiteSideViewModel filter)
        {
            var query = _context.Blog
              .AsNoTracking()
              .Where(s => !s.IsDelete && s.IsActive)
              .OrderByDescending(s => s.CreateDate)
              .AsQueryable();

            //Title
            if (!string.IsNullOrEmpty(filter.BlogTitle))
            {
                query = query.Where(p => p.BlogTitle.Contains(filter.BlogTitle));
            }


            //categoryId
            if (filter.BlogCatgeoryId.HasValue )
            {
                var categoryBlogs = _context.BlogSelectedCategories
                                .Include(p => p.Blog)
                                .Where(p => p.BlogCategoryId == filter.BlogCatgeoryId)
                                .Select(p => p.Blog)
                                .Distinct()
                                .AsQueryable();

                query = from q in query
                        join c in categoryBlogs
                        on q.BlogId equals c.BlogId
                        select new Blog
                        {
                            BlogId = q.BlogId,
                            BlogImageName = q.BlogImageName,
                            BlogTitle = q.BlogTitle,
                            CreateDate = q.CreateDate,
                            IsActive = q.IsActive,
                            LongDescription = q.LongDescription,
                            ShortDescription = q.ShortDescription,
                            UserId = q.UserId,
                            Tags = q.Tags
                        };
            }

            await filter.Paging(query);

            return filter;
        }

        //Fill Blog Single Page Site Side View Model
        public async Task<BlogSinglePageSiteSideViewModel> FillBlogSinglePageSiteSideViewModel(int blogId)
        {
            return await _context.Blog
                                 .AsNoTracking()
                                 .Include(p=> p.Users)
                                 .Where(p => !p.IsDelete && p.BlogId == blogId)
                                 .Select(p => new BlogSinglePageSiteSideViewModel()
                                 {
                                     Blog = p,
                                     BlogCategories = _context.BlogCategories
                                                              .AsNoTracking()
                                                              .Where(p => !p.IsDelete)
                                                              .ToList(),
                                     LastestBlogs = _context.Blog
                                                            .AsNoTracking()
                                                            .Where(p=> !p.IsDelete && p.IsActive)
                                                            .OrderByDescending(p=> p.CreateDate)
                                                            .Take(4)
                                                            .ToList(),
                                     Comments = _context.Comment
                                                        .Include(p=> p.Users)
                                                        .AsNoTracking()
                                                        .Where(p=> !p.IsDelete && p.BlogId.HasValue && p.BlogId == blogId && !p.IsAdminRead)
                                                        .OrderByDescending(p=> p.CreateDate)
                                                        .ToList(),
                                 })
                                 .FirstOrDefaultAsync();
        }

        #endregion
    }
}
