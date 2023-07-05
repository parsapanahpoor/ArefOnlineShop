#region Using

using Domain.ViewModels.UserPanel.FavoriteProducts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.Interfaces
{
    public interface IFavoriteProductRepository
    {
        #region User Panel Side

        //Fill FavoriteProductsUserPanelSideViewModel
        Task<List<FavoriteProductsUserPanelSideViewModel>> FillFavoriteProductsUserPanelSideViewModel(int userId);

        #endregion

        #region Site Site

        //Is User Selected This Product For His Favorite Products
        Task<bool> IsUserSelectedThisProductForHisFavoriteProducts(int productId, int userId);

        //check That IS Exist Product By Id
        Task<bool> checkThatISExistProductById(int id);

        //Remove Favorite Product With Product Id And User Id
        Task RemoveFavoriteProductWithProductIdAndUserId(int productId, int userId);

        //Add Product Favorite With productId And User Id 
        Task AddProductFavoriteWithproductIdAndUserId(int productId, int userId);

        #endregion
    }
}
