#region Usings


using Domain.Models.Wallet;
using Domain.ViewModels.Admin.Wallet;
using Domain.ViewModels.UserPanel.Wallet;
using System.Threading.Tasks;

#endregion

namespace Domain.Interfaces
{
    public interface IWalletRepository
    {
        #region Wallet

        Task<FilterWalletViewModel> FilterWalletsAsync(FilterWalletViewModel filter);

        Task<Wallet?> GetWalletByWalletIdAsync(ulong walletId);

        Task<int> GetSumUserWalletAsync(int userId);

        Task<AdminEditWalletViewModel?> GetWalletForEditAsync(ulong walletId);

        Task CreateWalletAsync(Wallet wallet);

        Task EditWalletAsync(Wallet wallet);

        Task ConfirmPayment(ulong payId, string authority, string refId);

        Task<Wallet> GetWalletById(ulong id);

        Task<ulong> CreateWallet(Wallet charge);

        Task<int> GetUserTotalDepositTransactions(int userId);

        Task<int> GetUserTotalWithdrawTransactions(int userId);

        Task<int> GetUserWalletBalance(int userId);

        //Get Home Visit Transaction For Cancelation Home Visit Request 
        Task<Wallet?> GetHomeVisitTransactionForCancelationHomeVisitRequest(ulong requestId);

        //Create Wallet Without Calculate
        Task CreateWalletWithoutCalculate(Wallet wallet);

        //Create Wallet Data
        Task CreateWalletData(WalletData walletData);

        //Find Wallet Transaction For Redirect To The Bank Portal 
        Task<Wallet?> FindWalletTransactionForRedirectToTheBankPortal(int userId, GatewayType gateway, ulong? requestId, string authority, int amount);

        //Update Wallet With Calculate Balance
        Task UpdateWalletWithCalculateBalance(Wallet wallet);

        #endregion

        #region Save Changes

        Task SaveChangesAsync();

        #endregion

        #region User Panel 

        Task<FilterWalletUserPnelViewModel> FilterWalletsAsyncUserPanel(FilterWalletUserPnelViewModel filter);

        #endregion
    }

}

