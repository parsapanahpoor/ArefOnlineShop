using Data.Context;
using Domain.Interfaces;
using Domain.Models.Blog;
using Domain.Models.Comment;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private ParsaWorkShopContext _context;
        public CommentRepository(ParsaWorkShopContext context)
        {
            _context = context;
        }

        public void AddComment(Comment comment)
        {
            _context.Comment.Add(comment);
            _context.SaveChanges();
        }

        public List<Comment> DeletedComments()
        {
            IQueryable<Comment> result = _context.Comment.IgnoreQueryFilters()
                            .Where(u => u.IsDelete && u.ProductTypeId == 2)                                                    
                             .Include(p => p.Users).Include(p => p.Blog);                                                   

            return result.ToList();
        }

        public List<Comment> DeletedVideoComments()
        {
            IQueryable<Comment> result = _context.Comment.IgnoreQueryFilters()
                                        .Where(u => u.IsDelete && u.ProductTypeId == 3)
                                         .Include(p => p.Users).Include(p => p.Blog);

            return result.ToList();
        }

        public List<Comment> GetAllBlogsComments()
        {
            return _context.Comment.Where(p => p.ProductTypeId == 2)
                            .Include(p => p.Users).Include(p => p.Blog)               
                            .OrderByDescending(p => p.CreateDate).ToList();               
        }

        public List<Comment> GetAllProductsComments()
        {
            return _context.Comment.Where(p => p.ProductTypeId == 1).Include(p=>p.Product)
                                        .Include(p => p.Users).Include(p => p.Blog)
                                        .OrderByDescending(p => p.CreateDate).ToList();
        }

        public List<Comment> GetAllVideosComments()
        {
            return _context.Comment.Where(p => p.ProductTypeId == 3)
                            .Include(p => p.Users).Include(p => p.Video)            
                            .OrderByDescending(p => p.CreateDate).ToList();            
        }

        public List<Comment> GetBlogsCommentsForShowByID(int id)
        {
            return _context.Comment.Include(c => c.Users).Where(c => !c.IsDelete && c.BlogId == id).ToList();
        }

        public List<Comment> GetCommentByBlogId(int id)
        {
            var comments = _context.Comment.Where(p => p.BlogId == id).Include(p => p.Users)
                                    .Include(p => p.Blog).OrderByDescending(p => p.CreateDate).ToList();   
                                      
            return comments;
        }

        public Comment GetCommentById(int id)
        {
            return _context.Comment.Where(p => p.CommentId == id).Include(p=>p.Product)
                            .Include(p => p.Blog).Include(p => p.Video).Include(p => p.Users)                
                             .First();               
        }

        public List<Comment> GetCommentByProductId(int id)
        {
            var comments = _context.Comment.Where(p => p.ProductID == id).Include(p => p.Users)
                                               .Include(p => p.Product).OrderByDescending(p => p.CreateDate).ToList();
            return comments;
        }

        public List<Comment> GetCommentByVideoId(int id)
        {
            var comments = _context.Comment.Where(p => p.VideoId == id).Include(p => p.Users)
                                   .Include(p => p.Video).OrderByDescending(p => p.CreateDate).ToList();               
                                                  
            return comments;
        }

        public int GetPageCount(int Blogid , int take)
        {
            return _context.Comment.Where(c => !c.IsDelete && c.BlogId == Blogid).Count() / take;
        }

        public List<Comment> GetProductCommentsForShowByID(int id)
        {
            return _context.Comment.Include(c => c.Users).Where(c => !c.IsDelete && c.ProductID == id).ToList();
        }

        public int GetProductPageCount(int ProductID, int take)
        {
            return _context.Comment.Where(c => !c.IsDelete && c.ProductID == ProductID).Count() / take;
        }

        public List<Comment> GetVideoCommentsForShowByID(int id)
        {
            return _context.Comment.Include(c => c.Users).Where(c => !c.IsDelete && c.VideoId == id).ToList();
        }

        public int GetVideoPageCount(int VideoID, int take)
        {
            return _context.Comment.Where(c => !c.IsDelete && c.VideoId == VideoID).Count() / take;
        }

        public void Savechanges()
        {
            _context.SaveChanges();
        }

        public void UpdateComment(Comment comment)
        {
            _context.Comment.Update(comment);
            Savechanges();
        }

        //Get List Of User Comments
        public async Task<List<Comment>> GetListOfUserComments(int userId)
        {
            return await _context.Comment
                                 .Include(p=> p.Users)
                                 .Include(p=> p.Product)
                                 .Include(p=> p.Blog)
                                 .AsNoTracking()
                                 .Where(p=> !p.IsDelete && p.UserId == userId)
                                 .ToListAsync();
        }
    }
}
