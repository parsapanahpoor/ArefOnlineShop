using Domain.Models.Users;
using Domain.Models.Wallet;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Domain.ViewModels.Admin.Wallet
{
   public class AdminCreateWalletViewModel
{
    #region Properties

    [DisplayName("User")]
    [Required(ErrorMessage = "Please Enter {0}")]
    public int UserId { get; set; }

    [DisplayName("Transaction Type")]
    [Required(ErrorMessage = "Please Enter {0}")]
    public TransactionType TransactionType { get; set; }

    [DisplayName("Gateway Type")]
    [Required(ErrorMessage = "Please Enter {0}")]
    public GatewayType GatewayType { get; set; }

    [DisplayName("Payment Type")]
    [Required(ErrorMessage = "Please Enter {0}")]
    public PaymentType PaymentType { get; set; }

    [DisplayName("Price")]
    [Required(ErrorMessage = "Please Enter {0}")]
    public int Price { get; set; }

    [DisplayName("Description")]
    [AllowNull]
    [MaxLength(500, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    public string? Description { get; set; }

    #endregion

    #region Display Properties

    [AllowNull]
    public User? User { get; set; }

    #endregion

}

public enum AdminCreateWalletResponse
{
    Success, UserNotFound, 
} 
}

