using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Wallet
{
    public enum TransactionType
    {
        [Display(Name = "Deposit")]
        Deposit = 0,

        [Display(Name = "Withdraw")]
        Withdraw = 1,
    }
}
