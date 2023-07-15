using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.SiteSide.Blog
{
    public class BlogSinglePageSiteSideViewModel
    {
        #region properties

        public Domain.Models.Blog.Blog Blog { get; set; }

        public List<Domain.Models.Blog.BlogCategory> BlogCategories { get; set; }

        public List<Domain.Models.Comment.Comment> Comments { get; set; }

        public List<Domain.Models.Blog.Blog> LastestBlogs { get; set; }

        #endregion
    }
}
