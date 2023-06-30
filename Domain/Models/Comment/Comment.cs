using Domain.Models.Blog;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Comment
{
    public class Comment
    {

        [Key]
        public int CommentId { get; set; }

        public int ProductTypeId { get; set; }

        public int UserId { get; set; }

        public int? ParentId { get; set; }

        public int? ProductID { get; set; }

        public int? VideoId { get; set; }

        public int? BlogId { get; set; }

        [MaxLength(700)]
        public string CommentBody { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }

        public bool IsAdminRead { get; set; }


        #region Relations

        [ForeignKey("ParentId")]
        public List<Comment> comments { get; set; }
        public  ProductType ProductType { get; set; }
        public  User Users { get; set; }
        public  Video Video { get; set; }
        public  Blog.Blog Blog { get; set; }
        public  Product.Product Product { get; set; }

        #endregion

    }
}
