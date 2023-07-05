#region Using

using Application.Extensions;
using Application.StaticTools;
using Domain.ViewModels.Admin.SiteSetting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        #endregion
    }
}
