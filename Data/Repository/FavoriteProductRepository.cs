#region Using

using Data.Context;
using Domain.Interfaces;
using Domain.Models.FavoriteProducts;
using Domain.Models.Product;
using Domain.Models.Users;
using Domain.ViewModels.UserPanel.FavoriteProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Data.Repository
{
    public class FavoriteProductRepository : IFavoriteProductRepository
    {
        #region Ctor

        private readonly ParsaWorkShopContext _context;

        public FavoriteProductRepository(ParsaWorkShopContext context)
        {
                _context = context;
        }

        #endregion

        #region User Panel 

        //Fill FavoriteProductsUserPanelSideViewModel
        public async Task<List<FavoriteProductsUserPanelSideViewModel>> FillFavoriteProductsUserPanelSideViewModel(int userId)
        {
            List<FavoriteProductsUserPanelSideViewModel> model = new List<FavoriteProductsUserPanelSideViewModel>();

            //Get Products Ids
            var productIds = await _context.FavoriteProducts
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.UserId == userId)
                                 .Select(p => p.ProductId)
                                 .ToListAsync();

            if (productIds != null && productIds.Any())
            {
                foreach (var productId in productIds)
                {
                    var childModel = await _context.product
                                                   .AsNoTracking()
                                                   .Where(p => !p.IsDelete && p.ProductID == productId)
                                                   .Select(p => new FavoriteProductsUserPanelSideViewModel()
                                                   {
                                                       ProductId = productId,
                                                       ProductImage = p.ProductImageName,
                                                       ProductName = p.ProductTitle,
                                                       SeoTitle = p.ProductTitle.Replace(" " , "-"),
                                                       CreateDate = p.CreateDate
                                                   })
                                                   .FirstOrDefaultAsync();
                    if (childModel != null)
                    {
                        model.Add(childModel);
                    }
                }
            }

            return model;
        }

        #endregion

        #region Site Site

        //Is User Selected This Product For His Favorite Products
        public async Task<bool> IsUserSelectedThisProductForHisFavoriteProducts(int productId , int userId)
        {
            return await _context.FavoriteProducts
                                 .AsNoTracking()
                                 .AnyAsync(p => !p.IsDelete && p.ProductId == productId && p.UserId == userId);
        }

        //check That IS Exist Product By Id
        public async Task<bool> checkThatISExistProductById(int id)
        {
            return await _context.product
                                 .AsNoTracking()
                                 .AnyAsync(p=> !p.IsDelete && p.ProductID == id);
        }

        //Remove Favorite Product With Product Id And User Id
        public async Task RemoveFavoriteProductWithProductIdAndUserId(int productId , int userId)
        {
            var favorite = await _context.FavoriteProducts
                                         .FirstOrDefaultAsync(p => !p.IsDelete && p.ProductId == productId && p.UserId == userId);
            
            favorite.IsDelete = true;

            //Update Method 
            _context.FavoriteProducts.Update(favorite);
            await _context.SaveChangesAsync();
        }

        //Add Product Favorite With productId And User Id 
        public async Task AddProductFavoriteWithproductIdAndUserId(int productId , int userId)
        {
            //Fill Entity
            FavoriteProducts favorite = new FavoriteProducts()
            {
                CreateDate = DateTime.Now,
                ProductId = productId,
                UserId = userId,
                IsDelete = false
            };

            //Add To The Data Base 
            await _context.FavoriteProducts.AddAsync(favorite);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
