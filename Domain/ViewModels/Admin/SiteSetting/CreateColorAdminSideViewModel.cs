using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Admin.SiteSetting
{
    public class CreateColorAdminSideViewModel
    {
        #region properties

        public string ColorName { get; set; }

        public string ColorCode { get; set; }

        #endregion
    }
}
