using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class OrderCancelationRequestDetail : BaseEntity
    {
        #region properties

        public int OrderId { get; set; }

        public OrderCancelationRequestState OrderCancelationRequestState { get; set; }

        public string DeclineDescription { get; set; }

        #endregion
    }

    public enum OrderCancelationRequestState
    {
        Pending,
        Accepted,
        Rejected
    }
}
