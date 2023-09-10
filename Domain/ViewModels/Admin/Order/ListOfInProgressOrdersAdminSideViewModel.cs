using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Admin.Order
{
    public class ListOfInProgressOrdersAdminSideViewModel
    {
        #region properties

        public List<Orders> Orders { get; set; }

        #endregion
    }
}
