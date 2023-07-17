using Domain.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICommentRepository
    {
        void Savechanges();

        void AddComment(Comment comment);

        int GetPageCount(int Blogid , int take);

        List<Comment> GetBlogsCommentsForShowByID(int id);

        int GetVideoPageCount(int VideoID, int take);

        List<Comment> GetVideoCommentsForShowByID(int id);

        int GetProductPageCount(int ProductID, int take);

        List<Comment> GetProductCommentsForShowByID(int id);

        //Get List Of User Comments
        Task<List<Comment>> GetListOfUserComments(int userId);


        #region PanelAdmin

        List<Comment> GetAllBlogsComments();
        List<Comment> GetAllProductsComments();

        List<Comment> GetAllVideosComments();

        Comment GetCommentById(int id);

        void UpdateComment(Comment comment);

        List<Comment> DeletedComments();

        List<Comment> DeletedVideoComments();

        List<Comment> GetCommentByBlogId(int id);
        List<Comment> GetCommentByProductId(int id);

        List<Comment> GetCommentByVideoId(int id);

        #endregion
    }
}
