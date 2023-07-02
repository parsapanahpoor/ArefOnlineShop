
using Domain.Models.Wallet;
using Domain.ViewModels.Admin.Wallet;
using Domain.ViewModels.UserPanel.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IWalletService
    {
        #region Wallet

        Task<bool> PayOrderAmount(int userId, int price, int orderId);

        Task<FilterWalletViewModel> FilterWalletsAsync(FilterWalletViewModel filter);

        Task<int?> GetSumUserWalletAsync(int userId);

        Task<AdminEditWalletViewModel?> GetWalletForEditAsync(int walletId);

        Task<AdminCreateWalletResponse> CreateWalletAsync(AdminCreateWalletViewModel model);

        Task<AdminEditWalletResponse> EditWalletAsync(AdminEditWalletViewModel model);

        Task ConfirmPayment(int payId, string authority, string refId);

        Task<bool> RemoveWalletAsync(int walletId);

        Task<int> ChargeUserWallet(AdminCreateWalletViewModel wallet);

        Task<WalletViewModel> GetWalletById(int id);

        //Create New Wallet Transaction For Redirext To The Bank Portal
        Task CreateNewWalletTransactionForRedirextToTheBankPortal(int userId, int price, GatewayType gateway, string authority, string description, int? requestId);

        //Find Wallet Transaction For Redirect To The Bank Portal 
        Task<Wallet?> FindWalletTransactionForRedirectToTheBankPortal(int userId, GatewayType gateway, int? requestId, string authority, int amount);

        //Update Wallet And Calculate User Balance After Banking Payment
        Task UpdateWalletAndCalculateUserBalanceAfterBankingPayment(Wallet wallet);

        #endregion

        #region User Panel 

        Task<FilterWalletUserPnelViewModel> FilterWalletsAsyncUserPanel(FilterWalletUserPnelViewModel filter);

        #endregion
    }
}
