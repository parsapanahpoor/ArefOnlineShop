using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models.Comment;
using Domain.Models.Users;
using Domain.ViewModels.SiteSide.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private ICommentRepository _comment;

        public CommentService(ICommentRepository comment)
        {
            _comment = comment;
        }

        public void AddComment(Comment comment , int userid , int ProductTypeId)
        {

            comment.IsDelete = false;
            comment.IsAdminRead = false;
            comment.CreateDate = DateTime.Now;
            comment.ProductTypeId = ProductTypeId;
            comment.UserId = userid;

            _comment.AddComment(comment);
        }

        //Add Comment For Blog
        public async Task AddCommmentForBlog(AddCommentForBlogsSiteSideViewModel model , int userId)
        {
            Comment comment = new Comment();

            comment.IsDelete = false;
            comment.IsAdminRead = false;
            comment.CreateDate = DateTime.Now;
            comment.ProductTypeId = 2;
            comment.UserId = userId;
            comment.BlogId = model.BlogId;
            comment.ParentId = model.ParentId;
            comment.CommentBody = model.CommentBody;

            _comment.AddComment(comment);
        }

        //Add Comment For Blog
        public async Task AddCommmentForProduct(AddCommentForBlogsSiteSideViewModel model, int userId)
        {
            Comment comment = new Comment();

            comment.IsDelete = false;
            comment.IsAdminRead = false;
            comment.CreateDate = DateTime.Now;
            comment.ProductTypeId = 1;
            comment.UserId = userId;
            comment.ProductID = model.BlogId;
            comment.ParentId = model.ParentId;
            comment.CommentBody = model.CommentBody;

            _comment.AddComment(comment);
        }

        public void DeleteComment(int id)
        {
            var comment = GetCommentById(id);
            comment.IsDelete = true;

            _comment.UpdateComment(comment);
        }

        public List<Comment> DeletedComments()
        {
            return DeletedComments();
        }

        public List<Comment> DeletedVideoComments()
        {
            return _comment.DeletedVideoComments();
        }

        public List<Comment> GetAllBlogsComments()
        {
            return _comment.GetAllBlogsComments();
        }

        public List<Comment> GetAllProductsComments()
        {
            return _comment.GetAllProductsComments();
        }

        public List<Comment> GetAllVideosComments()
        {
            return _comment.GetAllVideosComments();
        }

        public Tuple<List<Comment>, int> GetBlogComment(int BlogId, int pageId = 1)
        {
            int take = 10;
            int skip = (pageId - 1) * take;
            int pageCount = _comment.GetPageCount(BlogId , pageId);

            if ((pageCount % 2) != 0)
            {
                pageCount += 1;
            }

            List<Comment> commenst = _comment.GetBlogsCommentsForShowByID(BlogId);

            return Tuple.Create( commenst.Skip(skip).Take(take).OrderByDescending(c => c.CreateDate).ToList(), pageCount);
                    
        }

        public List<Comment> GetCommentByBlogId(int id)
        {
            return _comment.GetCommentByBlogId(id);
        }

        public Comment GetCommentById(int id)
        {
            return _comment.GetCommentById(id);
        }

        public List<Comment> GetCommentByPRoductId(int id)
        {
            return _comment.GetCommentByProductId(id);
        }

        public List<Comment> GetCommentByVideoId(int id)
        {
            return _comment.GetCommentByVideoId(id);
        }

        public Tuple<List<Comment>, int> GetProductComment(int ProductId, int pageId = 1)
        {
            int take = 10;
            int skip = (pageId - 1) * take;
            int pageCount = _comment.GetProductPageCount(ProductId, pageId);

            if ((pageCount % 2) != 0)
            {
                pageCount += 1;
            }

            List<Comment> commenst = _comment.GetProductCommentsForShowByID(ProductId);

            return Tuple.Create(commenst.Skip(skip).Take(take).OrderByDescending(c => c.CreateDate).ToList(), pageCount);
        }

        public Tuple<List<Comment>, int> GetVideoComment(int videoid, int pageId = 1)
        {
            int take = 10;
            int skip = (pageId - 1) * take;
            int pageCount = _comment.GetVideoPageCount(videoid, pageId);

            if ((pageCount % 2) != 0)
            {
                pageCount += 1;
            }

            List<Comment> commenst = _comment.GetVideoCommentsForShowByID(videoid);

            return Tuple.Create(commenst.Skip(skip).Take(take).OrderByDescending(c => c.CreateDate).ToList(), pageCount);

        }

        public void UpdateComment(Comment comment)
        {
            _comment.UpdateComment(comment);
        }
    }
}
