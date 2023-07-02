
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

        Task<FilterWalletViewModel> FilterWalletsAsync(FilterWalletViewModel filter);

        Task<int?> GetSumUserWalletAsync(int userId);

        Task<AdminEditWalletViewModel?> GetWalletForEditAsync(ulong walletId);

        Task<AdminCreateWalletResponse> CreateWalletAsync(AdminCreateWalletViewModel model);

        Task<AdminEditWalletResponse> EditWalletAsync(AdminEditWalletViewModel model);

        Task ConfirmPayment(ulong payId, string authority, string refId);

        Task<bool> RemoveWalletAsync(ulong walletId);

        Task<ulong> ChargeUserWallet(AdminCreateWalletViewModel wallet);

        Task<WalletViewModel> GetWalletById(ulong id);

        //Create New Wallet Transaction For Redirext To The Bank Portal
        Task CreateNewWalletTransactionForRedirextToTheBankPortal(int userId, int price, GatewayType gateway, string authority, string description, int requestId);

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
