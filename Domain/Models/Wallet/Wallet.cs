using Domain.Models.Common;
using Domain.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Models.Wallet
{
    public class Wallet
    {
        #region ctor

        public Wallet()
        {
        }

        #endregion

        #region Properties

        [Key]
        public int WalletId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public bool IsDelete { get; set; } = false;

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

        public bool IsFinally { get; set; }

        public int? RequestId { get; set; }

        #endregion

        #region Relations

        public User User { get; set; }

        public WalletData WalletData { get; set; }

        #endregion
    }
    public enum TransactionWay
    {
        [Display(Name = "Online")] Online,

        [Display(Name = "Cash")] Cash,

        [Display(Name = "Cheque")] Cheque,

        [Display(Name = "Deposit to account")] DepositToAccount,

        [Display(Name = "POS machine")] Pos,

        [Display(Name = "Other")] Other,
    }
}
