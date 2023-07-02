namespace Domain.ViewModels.Admin.Wallet
{
    public class AdminEditWalletViewModel : AdminCreateWalletViewModel
    {
        public int WalletId { get; set; }
    }

    public enum AdminEditWalletResponse
    {
        Success, WalletNotFound
    }
}

