﻿#region Using

using Data.Context;
using Domain.Interfaces;
using Domain.Models.Product;
using Domain.Models.SiteSetting;
using Domain.ViewModels.Admin.SiteSetting;
using Domain.ViewModels.SiteSide.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Data.Repository
{
    public class SiteSettingRepsitory : ISiteSettingRepsitory
    {
        #region Ctor

        private readonly ParsaWorkShopContext _context;

        public SiteSettingRepsitory(ParsaWorkShopContext context)
        {
            _context = context;
        }

        #endregion

        #region Admin Side 

        #region Product Color

        //Fill List Of Product Colors Admin Side View Model
        public async Task<List<ListOfProductColorsAdminSideViewModel>> FillListOfProductColorsAdminSideViewModel()
        {
            return await _context.ProductColors
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete)
                                 .Select(p => new ListOfProductColorsAdminSideViewModel()
                                 {
                                     Id = p.Id,
                                     ProductColorImage = p.ColorImage,
                                     ProductName = p.ColorTitle
                                 })
                                 .ToListAsync();
        }

        //Add Color To The Data Base
        public async Task AddColorToTheDataBase(ProductColor color)
        {
            await _context.ProductColors.AddAsync(color);
            await _context.SaveChangesAsync();
        }

        //Fill Edit Color Admin Side View Model
        public async Task<EditColorAdminSideViewModel> FillEditColorAdminSideViewModel(int id)
        {
            return await _context.ProductColors
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.Id == id)
                                 .Select(p => new EditColorAdminSideViewModel()
                                 {
                                     ColorId = p.Id,
                                     ColorImageName = p.ColorImage,
                                     ColorName = p.ColorTitle,
                                     ColorCode = p.ColorCode,
                                     ColorFarsiName = p.ColorFarsiTitle
                                 })
                                 .FirstOrDefaultAsync();
        }

        //Get Color By Id 
        public async Task<ProductColor> GetColorById(int colorId)
        {
            return await _context.ProductColors
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(p => !p.IsDelete && p.Id == colorId);
        }

        //Update Color 
        public async Task UpdateColor(ProductColor color)
        {
            _context.ProductColors.Update(color);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Size 

        //Fill ListOfColorsAdminSideViewModel
        public async Task<List<ListOfColorsAdminSideViewModel>> FillListOfColorsAdminSideViewModel()
        {
            return await _context.ProductsSizes
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete)
                                 .Select(p => new ListOfColorsAdminSideViewModel()
                                 {
                                     Id = p.Id,
                                     SizeTitle = p.SizeTitle,
                                 })
                                 .ToListAsync();
        }

        //Add Size To The DataBase
        public async Task AddSizeToTheDataBase(ProductsSize size)
        {
            await _context.ProductsSizes.AddAsync(size);
            await _context.SaveChangesAsync();
        }

        //Fill EditSizeAdminSideViewModel
        public async Task<EditSizeAdminSideViewModel> FillEditSizeAdminSideViewModel(int id)
        {
            return await _context.ProductsSizes
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.Id == id)
                                 .Select(p => new EditSizeAdminSideViewModel()
                                 {
                                     SizeId = p.Id,
                                     SizeName = p.SizeTitle
                                 })
                                 .FirstOrDefaultAsync();
        }

        //Get Size By Id 
        public async Task<ProductsSize> GetSizeById(int id)
        {
            return await _context.ProductsSizes
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(p => !p.IsDelete && p.Id == id);
        }

        //Update Size 
        public async Task UpdateSize(ProductsSize size)
        {
            _context.ProductsSizes.Update(size);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Site Setting

        public async Task<string> GetAdminMobilePhone()
        {
            return await _context.SiteSetting
                                 .AsNoTracking()
                                 .Select(p=> p.AdminMobile)
                                 .FirstOrDefaultAsync();
        }

        public async Task<SiteSetting?> GetSiteSetting(CancellationToken cancellationToken)
        {
            return await _context.SiteSetting.FirstOrDefaultAsync(p=> !p.IsDelete);
        }

        public async Task AddSiteSetting(SiteSetting siteSetting , CancellationToken cancellationToken)
        {
            await _context.SiteSetting.AddAsync(siteSetting);
        }

        public void UpdateSiteSetting(SiteSetting siteSetting)
        {
            _context.SiteSetting.Update(siteSetting);
        }

        public async Task SaveChangesAsync(CancellationToken cancellation)
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #endregion

        #region Site Side 

        //Fill Index Page View Model
        public async Task<IndexPageViewModel> FillIndexPageViewModel(int? userId)
        {
            IndexPageViewModel model = new IndexPageViewModel();

            #region Lastest Aref's Products

            List<LastestsArefProducts> lastProcuctsChild = new List<LastestsArefProducts>();

            var LastestCategoriesId = await _context.ProductCategories
                                                 .AsNoTracking()
                                                 .Where(p => !p.IsDelete&& p.ShowInPart1)
                                                 .ToListAsync();

            #region First Record For All Products

            LastestsArefProducts AllProducts = new LastestsArefProducts();

            AllProducts.ProdudctCategoryId = null;
            AllProducts.LastestProducts = await _context.product
                                                        .AsNoTracking()     
                                                        .Where(p=> !p.IsDelete && p.IsActive)
                                                        .OrderByDescending(p=> p.CreateDate)
                                                        .Select(p=> new LastestProducts()
                                                        {
                                                            IsInOffer = p.IsInOffer,
                                                            OldPrice = p.OldPrice,
                                                            Price = p.Price,
                                                            ProductId = p.ProductID,
                                                            ProductImageName = p.ProductImageName,
                                                            Title = p.ProductTitle,
                                                            SecondeProductImageName = _context.ProductGallery
                                                                                              .AsNoTracking()
                                                                                              .Where(s=> s.ProductID == p.ProductID && s.ShowForSecondeMainImage)
                                                                                              .Select(s=> s.ImageName)
                                                                                              .FirstOrDefault(),
                                                            IsInFavorite = !userId.HasValue ?
                                                                                false 
                                                                                : 
                                                                                _context.FavoriteProducts.Any(s=> !s.IsDelete && s.UserId == userId.Value && s.ProductId == p.ProductID) 
                                                        })
                                                        .Take(8)
                                                        .ToListAsync();

            lastProcuctsChild.Add(AllProducts);

            #endregion

            if (LastestCategoriesId != null && LastestCategoriesId.Any())
            {
                foreach (var categoryId in LastestCategoriesId)
                {
                    LastestsArefProducts lastestsArefProducts = new LastestsArefProducts();

                    lastestsArefProducts.ProdudctCategoryId = categoryId.ProductCategoryId;
                    lastestsArefProducts.ProdudctCategoryTitle = categoryId.CategoryTitle;

                    //Get Products Selected This Category
                    var productsId = await _context.ProductSelectedCategory
                                                   .AsNoTracking()
                                                   .Where(p => p.ProductCategoryId == categoryId.ProductCategoryId )
                                                   .OrderByDescending(p => p.CreateDate)
                                                   .Select(p => p.ProductID)
                                                   .Take(8)
                                                   .ToListAsync();

                    if (productsId != null && productsId.Any())
                    {
                        List<LastestProducts> productChildViewModel = new List<LastestProducts>();

                        foreach (var productId in productsId)
                        {
                            LastestProducts productViewModel = new LastestProducts();

                            productViewModel = await _context.product
                                                             .AsNoTracking()
                                                             .Where(p => !p.IsDelete && p.ProductID == productId && p.IsActive)
                                                             .Select(p => new LastestProducts()
                                                             {
                                                                 IsInOffer = p.IsInOffer,
                                                                 OldPrice = p.OldPrice,
                                                                 Price = p.Price,
                                                                 ProductId = p.ProductID,
                                                                 ProductImageName = p.ProductImageName,
                                                                 Title = p.ProductTitle,
                                                                 SecondeProductImageName = _context.ProductGallery
                                                                                              .AsNoTracking()
                                                                                              .Where(s => s.ProductID == p.ProductID && s.ShowForSecondeMainImage)
                                                                                              .Select(s => s.ImageName)
                                                                                              .FirstOrDefault(),
                                                                 IsInFavorite = !userId.HasValue ?
                                                                                false
                                                                                :
                                                                                _context.FavoriteProducts.Any(s => !s.IsDelete && s.UserId == userId.Value && s.ProductId == p.ProductID)
                                                             })
                                                             .FirstOrDefaultAsync();

                            if (productViewModel != null) productChildViewModel.Add(productViewModel);
                        }

                        if (productChildViewModel != null) lastestsArefProducts.LastestProducts = productChildViewModel;
                    }

                    if (lastestsArefProducts != null) lastProcuctsChild.Add(lastestsArefProducts);
                }
            }

            model.LastestsArefProducts = lastProcuctsChild;

            #endregion

            #region Categories With Images 

            model.LatestCategoriesWithImages =   await _context.ProductCategories
                                                               .AsNoTracking()
                                                               .Where(p => !p.IsDelete && !string.IsNullOrEmpty(p.ImageName) && p.ShowInPart2)
                                                               .OrderBy(p=> p.Priority)
                                                               .Select(p=> new LatestCategoriesWithImage()
                                                               {
                                                                   CategoryId = p.ProductCategoryId,
                                                                   CategoryTitle = p.CategoryTitle,
                                                                   ProductImage = p.ImageName,
                                                                   Div = p.DivTagClass,
                                                                   P= p.PTagClass
                                                               })
                                                               .ToListAsync();

            #endregion

            #region Users Comments

            model.UsersComments = await _context.UsersCommentsAboutSites
                                                .AsNoTracking()
                                                .Where(p => !p.IsDelete)
                                                .OrderByDescending(p=> p.CreateDate)
                                                .Select(p => new UsersCommentsAboutWebSites()
                                                {
                                                    CommentId = p.Id,
                                                    UserCommentText = p.CommentText,
                                                    Username = p.Username
                                                })
                                                .ToListAsync();

            #endregion

            #region Slider

            model.Sliders = await _context.Slider
                                          .AsNoTracking()
                                          .Where(p => !p.IsDelete)
                                          .OrderBy(p => p.Priority)
                                          .ToListAsync();

            #endregion

            return model;
        }

        #endregion
    }
}
