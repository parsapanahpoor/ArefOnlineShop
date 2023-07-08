using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Product
{
    public class ProductSelectedCategory
    {
        [Key]
        public int ProductSelectedCategoryID { get; set; }

        public int ProductCategoryId { get; set; }

        public int ProductID { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        #region Relations

        public ProductCategories ProductCategories { get; set; }
        public  Product Product { get; set; }

        #endregion

    }
}
