using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public  class ReturnedProducts
    {
        [Key]
        public int ReturnedProductID { get; set; }

        [ForeignKey("OrderDetails")]
        public int OrderDetailID { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [ForeignKey("ReturnedProductType")]
        public int ReturnProductTypeID { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        #region Navigations

        public Users.User Users { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public ReturnedProductType ReturnedProductType { get; set; }

        #endregion
    }
}
