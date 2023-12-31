﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public int Userid { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsFinally { get; set; }

        public int? DiscountUserSelected { get; set; }

        public int? Price { get; set; }

        [ForeignKey("Locations")]
        public int? LocationID { get; set; }

        public OrderState OrderState { get; set; } = OrderState.WaitingForPayment;

        #region Navigations

        public Users.User User { get; set; }
        public Users.Locations Locations { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public List<FinancialTransaction> FinancialTransactions { get; set; }

        #endregion

    }

    public enum OrderState
    {
        WaitingForPayment,
        InProccess,
        SentToTheCustomer,
        CancelationRequest,
        Canceled,
        Finally,
        ReturnedRequest,
        Returned
    }
}


