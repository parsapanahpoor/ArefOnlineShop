using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Admin.Order
{
    public class ListOfOrdersInOrderTrackingIndexPageAdminPanelViewModel
    {
        #region properties

        public List<Models.Order.Orders> Orders { get; set; }

        public int CountOfInProcessOrders { get; set; }

        public int CountOfCancelationRequests{ get; set; }

        public int CountOfFinishedOrders { get; set; }

        #endregion
    }
}
