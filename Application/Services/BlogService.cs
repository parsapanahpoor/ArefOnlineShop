using Application.Convertors;
using Application.Genarator;
using Application.Interfaces;
using Application.Security;
using Domain.Interfaces;
using Domain.Models.Blog;
using Domain.Models.Users;
using Domain.ViewModels.SiteSide.Blog;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BlogService : IBlogService
    {
        private IBlogRepository _blog;

        public BlogService(IBlogRepository blog)
        {
            _blog = blog;
        }

        public int AddBlog(Blog blog, IFormFile imgBlogUp, User user)
        {
            blog.UserId = user.UserId;
            blog.IsActive = true;
            blog.CreateDate = DateTime.Now;
            blog.BlogImageName = "no-photo.png";  //تصویر پیشفرض

            if (imgBlogUp != null && imgBlogUp.IsImage())
            {
                blog.BlogImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgBlogUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/image", blog.BlogImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgBlogUp.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", blog.BlogImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 320);
            }


            _blog.AddBlog(blog);

            return blog.BlogId;
        }

        public void AddBlogCategory(BlogCategory blogCategory)
        {
            _blog.AddBlogCategory(blogCategory);
        }

        public void AddCategoryToBlog(List<int> Categories, int BlogId)
        {
            foreach (var item in Categories)
            {
                BlogSelectedCategory blog = new BlogSelectedCategory()
                { 
                    BlogCategoryId = item,
                    BlogId = BlogId
                };

                _blog.AddCategoryToBlog(blog);

            }
        }

        public void AddCategoryToVideo(List<int> Categories, int videoid)
        {
            foreach (var item in Categories)
            {
                VideoSelectedCategory video = new VideoSelectedCategory()
                {
                    BlogCategoryId = item,
                    VideoId = videoid
                };

                _blog.AddCategoryToVideo(video);

            }
        }

        public int AddVideo(Video video, IFormFile imgBlogUp, IFormFile demoUp, User user)
        {
            video.UserId = user.UserId;
            video.IsActive = true;
            video.CreateDate = DateTime.Now;
            video.VideoImageName = "no-photo.png";  //تصویر پیشفرض


            //TODO Check Image
            if (imgBlogUp != null && imgBlogUp.IsImage())
            {
                video.VideoImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgBlogUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/image", video.VideoImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgBlogUp.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", video.VideoImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 320);
            }

            if (demoUp != null)
            {
                video.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(demoUp.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos", video.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    demoUp.CopyTo(stream);
                }
            }


            _blog.AddVideo(video);

            return video.VideoId;
        }

        public void DeleteBlog(Blog blog)
        {
            if (blog.BlogImageName != "no-photo.png")
            {
                string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/image", blog.BlogImageName);
                if (File.Exists(deleteimagePath))
                {
                    File.Delete(deleteimagePath);
                }

                string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", blog.BlogImageName);
                if (File.Exists(deletethumbPath))
                {
                    File.Delete(deletethumbPath);
                }
            }
            blog.BlogImageName = "no-photo.png";
            blog.IsDelete = true;
            _blog.UpdateBlog(blog);
        }

        public void DeleteBlogCategory(int id)
        {
            BlogCategory blogCategory = GetBlogCategoryById(id);
            blogCategory.IsDelete = true;

            _blog.UpdateBlogCategroy(blogCategory);
        }

        public void DeleteVideos(Video video)
        {
            if (video.VideoImageName != "no-photo.png")
            {
                string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/image", video.VideoImageName);
                if (File.Exists(deleteimagePath))
                {
                    File.Delete(deleteimagePath);
                }

                string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", video.VideoImageName);
                if (File.Exists(deletethumbPath))
                {
                    File.Delete(deletethumbPath);
                }
            }

            video.VideoImageName = "no-photo.png";

            video.IsDelete = true;
            _blog.UpdateVideo(video);
        }

        public void EditBlogSelectedCategory(List<int> Categories, int BlogId)
        {
            _blog.EditBlogSelectedCategory(BlogId);
            AddCategoryToBlog(Categories, BlogId);
        }

        public void EditeVideoSelectedCategory(List<int> Categories, int videoid)
        {
            _blog.EditeVideoSelectedCategory(videoid);

            AddCategoryToVideo(Categories, videoid);
        }

        public List<BlogCategory> GetAllBlogCategories()
        {
            return _blog.GetAllBlogCategories();
        }

        public List<Blog> GetAllBlogs()
        {
            return _blog.GetAllBlogs();
        }

        public List<BlogSelectedCategory> GetAllBlogSelectedCategory()
        {
            return _blog.GetAllBlogSelectedCategory();
        }

        public List<Blog> GetAllDeletedBlogs()
        {
            return _blog.GetAllDeletedBlogs();
        }

        public List<Video> GetAllDeletedVideos()
        {
            return _blog.GetAllDeletedVideos();
        }

        public List<Video> GetAllVideos()
        {
            return _blog.GetAllVideos();
        }

        public List<VideoSelectedCategory> GetAllVideoSelectedCategories()
        {
            return _blog.GetAllVideoSelectedCategories();
        }

        public Blog GetBlogById(int blogid)
        {
            return _blog.GetBlogById(blogid);
        }

        public BlogCategory GetBlogCategoryById(int id)
        {
            return _blog.GetBlogCategoryById(id);
        }

        public Tuple<List<Blog>, int> GetBlogsForShowInHomePage(int? Categroyid, int pageId = 1, string filter = "", int take = 0)
        {
            if (take == 0) take = 8;

            IQueryable<Blog> blogs = _blog.GetAllBlogsForIQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                blogs = _blog.SearchForBlog(filter);
            }

            if (Categroyid != null)
            {
                IQueryable<Blog> blogid = _blog.GetBlogsByCategoriID(Categroyid);

                if (Categroyid != null)
                {
                    if (blogid.Any() && blogid != null)
                    {
                        blogs = blogid;
                    }
                }
            }

            int skip = (pageId - 1) * take;
            int pageCount = (blogs.Count() / take);

            if ((pageCount % 2) != 0)
            {
                pageCount += 1;
            }

            var query = blogs.Skip(skip).Take(take).ToList();

            return Tuple.Create(query, pageCount);
        }

        public List<Blog> GetLastestBlogs()
        {
            if (_blog.BlogtCount() >= 4)
            {
                return _blog.GetLastestBlogIndexPageUpper4();
            }

            return _blog.GetLastestBlogIndexPageUnder4();
        }

        public List<Video> GetLastestVideos()
        {
            if (_blog.VideotCount() >= 4)
            {
                return _blog.GetLastestVideoIndexPageUpper4();
            }

            return _blog.GetLastestVideoIndexPageUnder4();
        }

        public string GetUserNameByBlog(int blogid)
        {
            return _blog.GetUserNameByBlog(blogid);
        }

        public Video GetVideoById(int VideoId)
        {
            return _blog.GetVideoById(VideoId);
        }

        public Tuple<List<Video>, int> GetVideosForShowInHomePage(int? Categroyid, int pageId = 1, string filter = "", int take = 0)
        {
            if (take == 0) take = 4;

            IQueryable<Video> blogs = _blog.GetAllVideosForIQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                blogs = _blog.SearchForVideo(filter);
            }

            if (Categroyid != null)
            {
                IQueryable<Video> blogid = _blog.GetVideoByCategoriID(Categroyid);

                if (Categroyid != null)
                {
                    if (blogid.Any() && blogid != null)
                    {
                        blogs = blogid;
                    }
                }
            }

            int skip = (pageId - 1) * take;
            int pageCount = (blogs.Count() / take);

            if ((pageCount % 2) != 0)
            {
                pageCount += 1;
            }

            var query = blogs.Skip(skip).Take(take).ToList();

            return Tuple.Create(query, pageCount);
        }

        public int UpdateBlog(Blog blog, IFormFile imgBlogUp)
        {
            //TODO Check Image
            if (imgBlogUp != null && imgBlogUp.IsImage())
            {

                if (blog.BlogImageName != "no-photo.png")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/image", blog.BlogImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", blog.BlogImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }

                blog.BlogImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgBlogUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/image", blog.BlogImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgBlogUp.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", blog.BlogImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 320);
            }

            _blog.UpdateBlog(blog);

            return blog.BlogId;
        }

        public void UpdateBlogCategroy(BlogCategory blogCategory)
        {
            _blog.UpdateBlogCategroy(blogCategory);
        }

        public void UpdateBlogForLock(Video video)
        {
            _blog.UpdateVideo(video);
        }

        public void UpdateBlogs(Blog blog)
        {
            _blog.UpdateBlog(blog);
        }

        public int UpdateVideo(Video video, IFormFile imgBlogUp, IFormFile demoUp)
        {
            if (imgBlogUp != null && imgBlogUp.IsImage())
            {

                if (video.VideoImageName != "no-photo.jpg")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/image", video.VideoImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", video.VideoImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }


                video.VideoImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgBlogUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/image", video.VideoImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgBlogUp.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/thumb", video.VideoImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 320);
            }

            if (demoUp != null)
            {

                if (video.DemoFileName != null)
                {
                    string deleteDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos", video.DemoFileName);
                    if (File.Exists(deleteDemoPath))
                    {
                        File.Delete(deleteDemoPath);
                    }
                }

                video.DemoFileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(demoUp.FileName);
                string demoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Blog/Videos", video.DemoFileName);
                using (var stream = new FileStream(demoPath, FileMode.Create))
                {
                    demoUp.CopyTo(stream);
                }
            }

            _blog.UpdateVideo(video);

            return video.VideoId;
        }

        #region Site Side 

        public async Task<ListOfBlogsSiteSideViewModel> FillListOfBlogsSiteSideViewModel(ListOfBlogsSiteSideViewModel filter)
        {
            return await _blog.FillListOfBlogsSiteSideViewModel(filter);
        }

        #endregion
    }
}
