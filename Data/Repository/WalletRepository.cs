#region Usings

using Domain.Interfaces;
using Domain.Models.Wallet;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using Data.Context;
using System.Linq;
using Domain.ViewModels.Admin.Wallet;
using Domain.ViewModels.UserPanel.Wallet;

#endregion

namespace DoctorFAM.Data.Repository
{
    public class WalletRepository : IWalletRepository
    {
        #region Ctor

        private readonly ParsaWorkShopContext _context;

        public WalletRepository(ParsaWorkShopContext context)
        {
            _context = context;
        }

        #endregion

        #region Wallet

        public async Task<FilterWalletViewModel> FilterWalletsAsync(FilterWalletViewModel filter)
        {
            var query = _context.Wallets
                .Include(w => w.User).Where(w => w.IsFinally)
                .AsQueryable();

            #region Order

            switch (filter.OrderType)
            {
                case FilterWalletViewModel.FilterWalletOrderType.Price:
                    query = query.OrderBy(w => w.Price).AsQueryable();
                    break;

                case FilterWalletViewModel.FilterWalletOrderType.PriceDesc:
                    query = query.OrderByDescending(w => w.Price).AsQueryable();
                    break;

                case FilterWalletViewModel.FilterWalletOrderType.CreateDate:
                    query = query.OrderBy(w => w.CreateDate).AsQueryable();
                    break;

                case FilterWalletViewModel.FilterWalletOrderType.CreateDateDesc:
                    query = query.OrderByDescending(w => w.CreateDate).AsQueryable();
                    break;

                default:
                    query = query.OrderByDescending(w => w.CreateDate).AsQueryable();
                    break;
            }

            #endregion

            #region Filters

            if (filter.UserId.HasValue)
            {
                query = query.Where(w => w.UserId == filter.UserId).AsQueryable();
            }
            else if (!string.IsNullOrEmpty(filter.UserFilter))
            {
                query = query.Where(w =>
                            EF.Functions.Like(w.User.Email, $"%{filter.UserFilter}%") ||
                            EF.Functions.Like(w.User.UserName, $"%{filter.UserFilter}%")
                        )
                    .AsQueryable();
            }

            if (filter.TransactionType.HasValue)
            {
                query = query.Where(w => w.TransactionType == filter.TransactionType).AsQueryable();
            }

            if (filter.GatewayType.HasValue)
            {
                query = query.Where(w => w.GatewayType == filter.GatewayType).AsQueryable();
            }

            if (filter.PaymentType.HasValue)
            {
                query = query.Where(w => w.PaymentType == filter.PaymentType).AsQueryable();
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(w => w.Price >= filter.MinPrice).AsQueryable();
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(w => w.Price <= filter.MaxPrice).AsQueryable();
            }

            if (filter.MinCreateDate.HasValue)
            {
                query = query.Where(w => w.CreateDate >= filter.MinCreateDate).AsQueryable();
            }

            if (filter.MaxCreateDate.HasValue)
            {
                query = query.Where(w => w.CreateDate <= filter.MaxCreateDate).AsQueryable();
            }

            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(w => w.Description != null && EF.Functions.Like(w.Description, $"%{filter.Description}%"));
            }

            if (filter.IsDelete.HasValue)
            {
                query = query.IgnoreQueryFilters().Where(w => w.IsDelete == filter.IsDelete).AsQueryable();
            }

            #endregion

            #region Paging

            await filter.Paging(query);

            #endregion

            return filter;
        }

        public Task<Wallet?> GetWalletByWalletIdAsync(ulong walletId)
        {
            return Task.FromResult(_context.Wallets.FirstOrDefault(w => w.Id == walletId));
        }

        public Task<int> GetSumUserWalletAsync(int userId)
        {
            return Task.FromResult(_context.Wallets.Where(u => u.UserId == userId && u.IsFinally).Sum(w => w.TransactionType == TransactionType.Deposit ? w.Price : w.Price * -1));
        }

        public async Task CreateWalletAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await SaveChangesAsync();

            //CalCulate User Wallet Balance
            var walletBalance = await GetUserWalletBalance(wallet.UserId);
        }

        //Update Wallet With Calculate Balance
        public async Task UpdateWalletWithCalculateBalance(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            await SaveChangesAsync();

            //CalCulate User Wallet Balance
            var walletBalance = await GetUserWalletBalance(wallet.UserId);
        }

        //Find Wallet Transaction For Redirect To The Bank Portal 
        public async Task<Wallet?> FindWalletTransactionForRedirectToTheBankPortal(int userId, GatewayType gateway, int? requestId, string authority, int amount)
        {
            return await _context.Wallets.Include(p => p.WalletData).FirstOrDefaultAsync(p => !p.IsDelete && !p.IsFinally && p.UserId == userId && p.GatewayType == gateway
                                                                                                && p.WalletData.TrackingCode == authority &&
                                                                                                        p.RequestId == requestId && p.Price == amount);
        }

        //Create Wallet Without Calculate
        public async Task CreateWalletWithoutCalculate(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await SaveChangesAsync();
        }

        //Create Wallet Data
        public async Task CreateWalletData(WalletData walletData)
        {
            await _context.WalletData.AddAsync(walletData);
            await SaveChangesAsync();
        }

        public async Task ConfirmPayment(ulong payId, string authority, string refId)
        {
            var payment = await _context.Wallets.FirstOrDefaultAsync(w => w.Id == payId);
            if (payment != null)
            {
                payment.IsFinally = true;

                _context.Wallets.Update(payment);
                await SaveChangesAsync();
                var paymentData = new WalletData()
                {
                    Token = authority,
                    WalletId = payId,
                    CreateDate = DateTime.Now,
                    GatewayType = payment.GatewayType,
                    TrackingCode = refId
                };

                _context.WalletData.Add(paymentData);
                await SaveChangesAsync();

            }
        }

        public async Task EditWalletAsync(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            await SaveChangesAsync();

            //CalCulate User Wallet Balance
            var walletBalance = await GetUserWalletBalance(wallet.UserId);
        }

        public Task<AdminEditWalletViewModel?> GetWalletForEditAsync(ulong walletId)
        {
            return Task.FromResult(_context.Wallets
                .Include(w => w.User)
                .Where(w => w.Id == walletId)
                .Select(w => new AdminEditWalletViewModel
                {
                    User = w.User,
                    Description = w.Description,
                    GatewayType = w.GatewayType,
                    PaymentType = w.PaymentType,
                    Price = w.Price,
                    TransactionType = w.TransactionType,
                    UserId = w.UserId,
                    WalletId = w.Id
                }).FirstOrDefault());
        }

        public async Task<Wallet?> GetWalletById(ulong id)
        {
            return await _context.Wallets.Include(w => w.User)
                .Include(w => w.WalletData)
                .FirstOrDefaultAsync(w => !w.IsDelete && w.Id == id && !w.IsFinally);
        }

        public async Task<ulong> CreateWallet(Wallet charge)
        {
            await _context.Wallets.AddAsync(charge);
            await SaveChangesAsync();
            return charge.Id;
        }

        public async Task<int> GetUserTotalDepositTransactions(int userId)
        {
            return _context.Wallets.Where(p => p.UserId == userId && p.IsFinally && !p.IsDelete && p.TransactionType == TransactionType.Deposit).Sum(p => p.Price);
        }

        public async Task<int> GetUserTotalWithdrawTransactions(int userId)
        {
            return _context.Wallets.Where(p => p.UserId == userId && p.IsFinally && !p.IsDelete && p.TransactionType == TransactionType.Withdraw).Sum(p => p.Price);
        }

        public async Task<int> GetUserWalletBalance(int userId)
        {
            //Get User Total Deposit Transactions
            var deposits = await GetUserTotalDepositTransactions(userId);

            //Get User Total Withdraw Transactions
            var withdraw = await GetUserTotalWithdrawTransactions(userId);

            //Update User Wallet Balance
            var user = await _context.Users.FirstOrDefaultAsync(p => p.UserId == userId && !p.IsDelete);
            user.WalletBalance = deposits - withdraw;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return deposits - withdraw;
        }

        #endregion

        #region Save Changes

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Site Side 

        //Get Home Visit Transaction For Cancelation Home Visit Request 
        public async Task<Wallet?> GetHomeVisitTransactionForCancelationHomeVisitRequest(ulong requestId)
        {
            return await _context.Wallets.Where(p => p.RequestId == requestId).FirstOrDefaultAsync();
        }

        #endregion

        #region User Panel 

        public async Task<FilterWalletUserPnelViewModel> FilterWalletsAsyncUserPanel(FilterWalletUserPnelViewModel filter)
        {
            var query = _context.Wallets
                .Include(w => w.User).Where(w => w.IsFinally && w.UserId == filter.UserId)
                .AsQueryable();

            #region Order

            switch (filter.OrderType)
            {
                case FilterWalletUserPnelViewModel.FilterWalletUserPanelOrderType.Price:
                    query = query.OrderBy(w => w.Price).AsQueryable();
                    break;

                case FilterWalletUserPnelViewModel.FilterWalletUserPanelOrderType.PriceDesc:
                    query = query.OrderByDescending(w => w.Price).AsQueryable();
                    break;

                case FilterWalletUserPnelViewModel.FilterWalletUserPanelOrderType.CreateDate:
                    query = query.OrderBy(w => w.CreateDate).AsQueryable();
                    break;

                case FilterWalletUserPnelViewModel.FilterWalletUserPanelOrderType.CreateDateDesc:
                    query = query.OrderByDescending(w => w.CreateDate).AsQueryable();
                    break;

                default:
                    query = query.OrderByDescending(w => w.CreateDate).AsQueryable();
                    break;
            }

            #endregion

            #region Filters

            if (filter.TransactionType.HasValue)
            {
                query = query.Where(w => w.TransactionType == filter.TransactionType).AsQueryable();
            }

            if (filter.GatewayType.HasValue)
            {
                query = query.Where(w => w.GatewayType == filter.GatewayType).AsQueryable();
            }

            if (filter.PaymentType.HasValue)
            {
                query = query.Where(w => w.PaymentType == filter.PaymentType).AsQueryable();
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(w => w.Price >= filter.MinPrice).AsQueryable();
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(w => w.Price <= filter.MaxPrice).AsQueryable();
            }

            if (filter.MinCreateDate.HasValue)
            {
                query = query.Where(w => w.CreateDate >= filter.MinCreateDate).AsQueryable();
            }

            if (filter.MaxCreateDate.HasValue)
            {
                query = query.Where(w => w.CreateDate <= filter.MaxCreateDate).AsQueryable();
            }

            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(w => w.Description != null && EF.Functions.Like(w.Description, $"%{filter.Description}%"));
            }

            if (filter.IsDelete.HasValue)
            {
                query = query.IgnoreQueryFilters().Where(w => w.IsDelete == filter.IsDelete).AsQueryable();
            }

            #endregion

            #region Paging

            await filter.Paging(query);

            #endregion

            return filter;
        }


        #endregion
    }

}

