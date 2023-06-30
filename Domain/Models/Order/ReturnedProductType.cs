using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class ReturnedProductType
    {
        [Key]
        public int ReturnedProductTypeID { get; set; }

        public string Title { get; set; }

        #region Navigation

        public List<ReturnedProducts> ReturnedProducts { get; set; }

        #endregion
    }
}
