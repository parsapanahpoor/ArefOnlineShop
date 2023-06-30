using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Blog
{
    public class BlogSelectedCategory
    {

        [Key]
        public int BlogSelectedCategoryId { get; set; }

        public int BlogCategoryId { get; set; }

        public int BlogId { get; set; }


        #region Relations

        public  BlogCategory BlogCategory { get; set; }
        public  Blog Blog { get; set; }

        #endregion
    }
}
