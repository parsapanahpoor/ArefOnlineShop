#region Using

using Domain.Models.Product;
using Domain.ViewModels.Admin.SiteSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.Interfaces
{
    public interface ISiteSettingRepsitory 
    {
        #region Admin Side 

        //Fill List Of Product Colors Admin Side View Model
        Task<List<ListOfProductColorsAdminSideViewModel>> FillListOfProductColorsAdminSideViewModel();

        //Add Color To The Data Base
        Task AddColorToTheDataBase(ProductColor color);

        //Fill Edit Color Admin Side View Model
        Task<EditColorAdminSideViewModel> FillEditColorAdminSideViewModel(int id);

        //Get Color By Id 
        Task<ProductColor> GetColorById(int colorId);

        //Update Color 
        Task UpdateColor(ProductColor color);

        #endregion
    }
}
