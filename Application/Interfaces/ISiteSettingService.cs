#region Using

using Application.Extensions;
using Application.StaticTools;
using Domain.Models.SiteSetting;
using Domain.ViewModels.Admin.SiteSetting;
using Domain.ViewModels.SiteSide.Home;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Application.Interfaces
{
    public interface ISiteSettingService
    {
        #region Admin Side 

        #region Product Color

        //Fill List Of Product Colors Admin Side View Model
        Task<List<ListOfProductColorsAdminSideViewModel>> FillListOfProductColorsAdminSideViewModel();

        //Add Color Admin Side
        Task<bool> AddColorAdminSide(CreateColorAdminSideViewModel color, IFormFile colorImage);

        //Fill Edit Color Admin Side View Model
        Task<EditColorAdminSideViewModel> FillEditColorAdminSideViewModel(int id);

        //Edit Color Admin Side 
        Task<bool> EditColorAdminSidel(EditColorAdminSideViewModel model, IFormFile? imgBlogUp);

        //Delete Color
        Task<bool> DeleteColor(int id);

        #endregion

        #region Size

        //Fill ListOfColorsAdminSideViewModel
        Task<List<ListOfColorsAdminSideViewModel>> FillListOfColorsAdminSideViewModel();

        //Create Size Admin Side 
        Task<bool> CreateSize(CreateSizeAdminSideViewMolde model);

        //Fill EditSizeAdminSideViewModel
        Task<EditSizeAdminSideViewModel> FillEditSizeAdminSideViewModel(int id);

        //edit Size 
        Task<bool> EditSize(EditSizeAdminSideViewModel model);

        //Delete Size 
        Task<bool> DeleteSize(int id);

        #endregion

        #region Site Setting

        Task<SiteSetting?> GetSiteSetting(CancellationToken cancellationToken);

        Task<bool> AddOrEditSiteSetting(SiteSetting newSiteSetting, CancellationToken cancellationToken);

        #endregion

        #endregion

        #region Site Side 

        //Fill Index Page View Model
        Task<IndexPageViewModel> FillIndexPageViewModel(int? userId);

        #endregion
    }
}
