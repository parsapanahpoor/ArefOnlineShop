﻿#region Using

using Application.Extensions;
using Application.Interfaces;
using Application.StaticTools;
using Data.Repository;
using Domain.Interfaces;
using Domain.Models.Product;
using Domain.Models.SiteSetting;
using Domain.Models.Slider;
using Domain.ViewModels.Admin.SiteSetting;
using Domain.ViewModels.Admin.Slider;
using Domain.ViewModels.SiteSide.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Application.SiteServices
{
    public class SiteSettingService : ISiteSettingService
    {
        #region Ctor

        private readonly ISiteSettingRepsitory _siteSettingRepsitory;

        public SiteSettingService(ISiteSettingRepsitory siteSettingRepsitory)
        {
            _siteSettingRepsitory = siteSettingRepsitory;
        }

        #endregion

        #region Admin Side 

        #region Product Color

        //Fill List Of Product Colors Admin Side View Model
        public async Task<List<ListOfProductColorsAdminSideViewModel>> FillListOfProductColorsAdminSideViewModel()
        {
            return await _siteSettingRepsitory.FillListOfProductColorsAdminSideViewModel();
        }

        //Add Color Admin Side
        public async Task<bool> AddColorAdminSide(CreateColorAdminSideViewModel color, IFormFile colorImage)
        {
            #region Fill Color 

            ProductColor model = new ProductColor()
            {
                ColorTitle = color.ColorName,
                ColorCode = color.ColorCode,
            };

            #endregion

            #region Add Color Image

            if (colorImage != null)
            {
                var imageName = Guid.NewGuid() + Path.GetExtension(colorImage.FileName);
                colorImage.AddImageToServer(imageName, PathTools.ColorPathServer, 400, 300, PathTools.ColorPathThumbServer);

                model.ColorImage = imageName;
            }

            #endregion

            //Add To The Data Base
            await _siteSettingRepsitory.AddColorToTheDataBase(model);

            return true;
        }

        //Fill Edit Color Admin Side View Model
        public async Task<EditColorAdminSideViewModel> FillEditColorAdminSideViewModel(int id)
        {
            return await _siteSettingRepsitory.FillEditColorAdminSideViewModel(id);
        }

        //Edit Color Admin Side 
        public async Task<bool> EditColorAdminSidel(EditColorAdminSideViewModel model, IFormFile? imgBlogUp)
        {
            #region Get Color By Id 

            var color = await _siteSettingRepsitory.GetColorById(model.ColorId);
            if (color == null) { return false; }

            #endregion

            #region Edit Color Fields

            color.ColorTitle = model.ColorName;
            color.ColorCode = model.ColorCode;
            color.ColorFarsiTitle = model.ColorFarsiName;

            #region Update Color Image

            if (imgBlogUp != null)
            {
                var imageName = Guid.NewGuid() + Path.GetExtension(imgBlogUp.FileName);
                imgBlogUp.AddImageToServer(imageName, PathTools.ColorPathServer, 400, 300, PathTools.ColorPathThumbServer);

                if (!string.IsNullOrEmpty(color.ColorImage))
                {
                    color.ColorImage.DeleteImage(PathTools.ColorPathServer, PathTools.ColorPathThumbServer);
                }

                color.ColorImage = imageName;
            }

            #endregion

            #endregion

            #region Update Method

            await _siteSettingRepsitory.UpdateColor(color);

            #endregion

            return true;
        }

        //Delete Color
        public async Task<bool> DeleteColor(int id)
        {
            #region Get Color By Id 

            var color = await _siteSettingRepsitory.GetColorById(id);
            if (color == null) { return false; }

            #endregion

            #region Edit Color Fields

            color.IsDelete = true;

            #region Update Color Image

            if (!string.IsNullOrEmpty(color.ColorImage))
            {
                color.ColorImage.DeleteImage(PathTools.ColorPathServer, PathTools.ColorPathThumbServer);
            }

            #endregion

            #endregion

            #region Update Method

            await _siteSettingRepsitory.UpdateColor(color);

            #endregion

            return true;
        }

        #endregion

        #region Size

        //Fill ListOfColorsAdminSideViewModel
        public async Task<List<ListOfColorsAdminSideViewModel>> FillListOfColorsAdminSideViewModel()
        {
            return await _siteSettingRepsitory.FillListOfColorsAdminSideViewModel();
        }

        //Create Size Admin Side 
        public async Task<bool> CreateSize(CreateSizeAdminSideViewMolde model)
        {
            #region Fill Entity

            ProductsSize size = new ProductsSize()
            {
                SizeTitle = model.SizeName
            };

            #endregion

            //Add To The Data Base
            await _siteSettingRepsitory.AddSizeToTheDataBase(size);

            return true;
        }

        //Fill EditSizeAdminSideViewModel
        public async Task<EditSizeAdminSideViewModel> FillEditSizeAdminSideViewModel(int id)
        {
            return await _siteSettingRepsitory.FillEditSizeAdminSideViewModel(id);
        }

        //edit Size 
        public async Task<bool> EditSize(EditSizeAdminSideViewModel model)
        {
            #region Get Size 

            var size = await _siteSettingRepsitory.GetSizeById(model.SizeId);
            if (size == null) return false;

            #endregion

            #region Update 

            size.SizeTitle = model.SizeName;

            #endregion

            //Update Method 
            await _siteSettingRepsitory.UpdateSize(size);

            return true;
        }

        //Delete Size 
        public async Task<bool> DeleteSize(int id)
        {
            #region Get Size 

            var size = await _siteSettingRepsitory.GetSizeById(id);
            if (size == null) return false;

            #endregion

            #region Update 

            size.IsDelete = true;

            #endregion

            //Update Method 
            await _siteSettingRepsitory.UpdateSize(size);

            return true;
        }

        #endregion

        #region Site Setting

        public async Task<SiteSetting?> GetSiteSetting(CancellationToken cancellationToken)
        {
            return await _siteSettingRepsitory.GetSiteSetting(cancellationToken);
        }

        public async Task<bool> AddOrEditSiteSetting(SiteSetting newSiteSetting , CancellationToken cancellationToken)
        {
            #region Get Site Setting 

            var oldSiteSetting = await GetSiteSetting(cancellationToken);

            #endregion

            #region Add Or Edit Site Setting

            if (oldSiteSetting != null)
            {
                oldSiteSetting.AdminMobile = newSiteSetting.AdminMobile;

                _siteSettingRepsitory.UpdateSiteSetting(oldSiteSetting);
                await _siteSettingRepsitory.SaveChangesAsync(cancellationToken);
            }
            else
            {
                SiteSetting siteSetting = new SiteSetting()
                {
                    AdminMobile = newSiteSetting.AdminMobile,
                };

                await _siteSettingRepsitory.AddSiteSetting(siteSetting , cancellationToken);
                await _siteSettingRepsitory.SaveChangesAsync(cancellationToken);
            }

            return true;

            #endregion
        }

        #endregion

        #endregion

        #region Site Side 

        //Fill Index Page View Model
        public async Task<IndexPageViewModel> FillIndexPageViewModel(int? userId)
        {
            return await _siteSettingRepsitory.FillIndexPageViewModel(userId);
        }

        #endregion
    }
}
