using Domain.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICommentService
    {

        void AddComment(Comment comment , int userid , int ProductTypeId);
        Tuple<List<Comment>, int> GetBlogComment(int BlogId, int pageId = 1);
        Tuple<List<Comment>, int> GetVideoComment(int videoid, int pageId = 1);
        Tuple<List<Comment>, int> GetProductComment(int ProductId, int pageId = 1);

        #region AdminPanel

        List<Comment> GetAllBlogsComments();
        List<Comment> GetAllProductsComments();
        List<Comment> GetAllVideosComments();
        Comment GetCommentById(int id);
        void UpdateComment(Comment comment);
        void DeleteComment(int id);
        List<Comment> DeletedComments();
        List<Comment> DeletedVideoComments();
        List<Comment> GetCommentByBlogId(int id);
        List<Comment> GetCommentByPRoductId(int id);
        List<Comment> GetCommentByVideoId(int id);

        #endregion

    }
}
