using Domain.Models.Wallet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Admin.Wallet
{
    public class WalletViewModel
    {
        #region Properties

        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public GatewayType GatewayType { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }

        [Required]
        public TransactionWay TransactionWay { get; set; }

        [Required]
        public int Price { get; set; }

        [AllowNull]
        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public bool IsDelete { get; set; }
        public bool IsFinally { get; set; }

        [Display(Name = "TrackingCode")]
        public string? TrackingCode { get; set; }

        [Display(Name = "ReferenceNumber")]
        public string? ReferenceCode { get; set; }

        [Display(Name = "Token")]
        public string? Token { get; set; }

        #endregion
    }
}
