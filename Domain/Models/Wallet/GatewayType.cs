using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Wallet
{
    public enum GatewayType
    {
        [Display(Name = "ZarinPal")]
        Zarinpal = 0,

        [Display(Name = "AdminSystem")]
        System = 1,

        [Display(Name = "PayPal")]
        PayPal = 2,

        [Display(Name = "Stripe")]
        Stripe = 3
    }
}

