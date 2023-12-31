﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Admin.SiteSetting
{
    public class EditColorAdminSideViewModel
    {
        #region properties

        public int ColorId { get; set; }

        public string ColorImageName { get; set; }

        public string ColorName{ get; set; }

        public string ColorFarsiName{ get; set; }

        public string ColorCode { get; set; }

        #endregion
    }
}
