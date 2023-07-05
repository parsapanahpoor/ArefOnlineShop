#region Usings

using Application.Interfaces;
using Domain.Interfaces;
using Domain.ViewModels.UserPanel.FavoriteProducts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Application.Services
{
    public class FavoriteProductsService : IFavoriteProductsService
    {
        #region Ctor 

        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public FavoriteProductsService(IFavoriteProductRepository favoriteProductRepository)
        {
                _favoriteProductRepository = favoriteProductRepository;
        }

        #endregion

        #region User Panel

        //Fill FavoriteProductsUserPanelSideViewModel
        public async Task<List<FavoriteProductsUserPanelSideViewModel>> FillFavoriteProductsUserPanelSideViewModel(int userId)
        {
            return await _favoriteProductRepository.FillFavoriteProductsUserPanelSideViewModel(userId);
        }

        #endregion

        #region Site Site

        //Is User Selected This Product For His Favorite Products
        public async Task<bool> IsUserSelectedThisProductForHisFavoriteProducts(int productId, int userId)
        {
            return await _favoriteProductRepository.IsUserSelectedThisProductForHisFavoriteProducts(productId, userId);
        }

        //Add or Remove Product From Favorite
        public async Task<bool> AddorRemoveProductFromFavorite(int productId, int userId)
        {
            #region Get Product

            var productExist = await _favoriteProductRepository.checkThatISExistProductById(productId);
            if (!productExist) return false;

            #endregion

            #region Is Exist Favorite Product Wirh ProductId And User Id

            var isFavorite = await IsUserSelectedThisProductForHisFavoriteProducts(productId , userId);

            //Remove From Favorite
            if (isFavorite)
            {
                await _favoriteProductRepository.RemoveFavoriteProductWithProductIdAndUserId(productId, userId);
            }
            else
            {
                await _favoriteProductRepository.AddProductFavoriteWithproductIdAndUserId(productId , userId);
            }

            #endregion

            return true;
        }

        #endregion
    }
}
