namespace Domain.ViewModels.Admin.Wallet
{
    public class AdminEditWalletViewModel : AdminCreateWalletViewModel
    {
        public ulong WalletId { get; set; }
    }

    public enum AdminEditWalletResponse
    {
        Success, WalletNotFound
    }
}

