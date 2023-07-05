#region Using

using Domain.ViewModels.UserPanel.FavoriteProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Application.Interfaces
{
    public interface IFavoriteProductsService 
    {
        #region User Panel

        //Fill FavoriteProductsUserPanelSideViewModel
        Task<List<FavoriteProductsUserPanelSideViewModel>> FillFavoriteProductsUserPanelSideViewModel(int userId);

        #endregion

        #region Site Site

        //Is User Selected This Product For His Favorite Products
        Task<bool> IsUserSelectedThisProductForHisFavoriteProducts(int productId, int userId);

        //Add or Remove Product From Favorite
        Task<bool> AddorRemoveProductFromFavorite(int productId, int userId);

        #endregion
    }
}
