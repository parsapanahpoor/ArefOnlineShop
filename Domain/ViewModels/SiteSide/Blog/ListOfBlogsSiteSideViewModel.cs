using Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.SiteSide.Blog
{
    public class ListOfBlogsSiteSideViewModel : BasePaging<Domain.Models.Blog.Blog>
    {
        #region properties

        public string BlogTitle { get; set; }

        public int? BlogCatgeoryId { get; set; }

        #endregion
    }
}
