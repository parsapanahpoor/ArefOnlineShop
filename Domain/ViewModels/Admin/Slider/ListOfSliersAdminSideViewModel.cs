#region Using

using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.ViewModels.Admin.Slider
{
    public class ListOfSliersAdminSideViewModel
    {
        #region properties

        public int SliderId { get; set; }

        public string SliderImage { get; set; }

        public DateTime CreateDate { get; set; }

        public string ColorCode { get; set; }

        #endregion
    }
}
