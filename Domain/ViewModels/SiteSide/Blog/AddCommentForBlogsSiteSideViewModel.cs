using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.SiteSide.Blog
{
    public class AddCommentForBlogsSiteSideViewModel
    {
        #region propreties

        public int? ParentId { get; set; }

        public int? BlogId { get; set; }

        [MaxLength(700)]
        public string CommentBody { get; set; }

        #endregion
    }
}
