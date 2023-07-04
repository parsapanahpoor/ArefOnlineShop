using Domain.Models.Wallet;
using Domain.ViewModels.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels.Admin.Wallet
{
    public class FilterWalletViewModel : BasePaging<Domain.Models.Wallet.Wallet>
    {
        #region Filter Properties

        public int? UserId { get; set; }

        [DisplayName("User")]
        public string? UserFilter { get; set; }

        [DisplayName("Transaction Type")]
        public TransactionType? TransactionType { get; set; }

        [DisplayName("Gateway Type")]
        public GatewayType? GatewayType { get; set; }

        [DisplayName("Payment Type")]
        public PaymentType? PaymentType { get; set; }

        [DisplayName("Min Price")]
        public int? MinPrice { get; set; }

        [DisplayName("Max Price")]
        public int? MaxPrice { get; set; }

        [DisplayName("From Date")]
        public DateTime? MinCreateDate { get; set; }

        [DisplayName("To Date")]
        public DateTime? MaxCreateDate { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayName("Deleted ?")]
        public bool? IsDelete { get; set; }

        #endregion

        #region Order Properties

        [DisplayName("Sort By")]
        public FilterWalletOrderType OrderType { get; set; }

        #endregion

        public enum FilterWalletOrderType
        {
            [Display(Name = "Create Date Descending")]
            CreateDateDesc = 1,

            [Display(Name = "Create Date")]
            CreateDate = 2,

            [Display(Name = "Price")]
            Price = 3,

            [Display(Name = "Price Descending")]
            PriceDesc = 4,
        }
    }
}

