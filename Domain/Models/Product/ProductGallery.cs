using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Product
{
    public class ProductGallery
    {
        [Key]
        public int ProductGalleryId { get; set; }
        public int ProductID { get; set; }

        [Display(Name = "عنوان تصویر    ")]
        [MaxLength(800, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }

        [MaxLength(50)]
        public string ImageName { get; set; }

        public bool ShowForSecondeMainImage { get; set; }

        #region Relations

        public  Product Product { get; set; }

        #endregion

    }
}
