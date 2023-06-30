using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Blog
{
    public class VideoSelectedCategory
    {

        [Key]
        public int VideoSelectedCategoryId { get; set; }

        public int BlogCategoryId { get; set; }

        public int VideoId { get; set; }

        #region Relations

        public virtual BlogCategory BlogCategory { get; set; }
        public virtual Video Video { get; set; }

        #endregion

    }
}
