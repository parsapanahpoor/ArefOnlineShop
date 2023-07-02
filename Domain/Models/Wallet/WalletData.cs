using System.ComponentModel.DataAnnotations;
using Domain.Models.Common;

namespace Domain.Models.Wallet
{
    public class WalletData : BaseEntity
    {
        public string? TrackingCode { get; set; }

        public string? ReferenceCode { get; set; }

        public string? Token { get; set; }

        public GatewayType GatewayType { get; set; }

        [Required]
        public int WalletId { get; set; }

        #region Relations

        public Wallet Wallet { get; set; }

        #endregion
    }
}
