#region Using

using Domain.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Application.Interfaces
{
    public interface ISliderService
    {
        #region Admin Panel

        //Fill List Of Sliers Admin Side View Model
        Task<List<ListOfSliersAdminSideViewModel>?> FillListOfSliersAdminSideViewModel();

        //Add Slider Admin Side
        Task<bool> AddSliderAdminSide(CreateSliderAdminSideViewModel slider, IFormFile sliderImage);

        //Fill Edit Slider View Model
        Task<EditSliderViewModel?> FillEditSliderViewModel(int sliderId);

        //Edit Slider Admin Side 
        Task<bool> EditSliderAdminSidel(EditSliderViewModel model, IFormFile? imgBlogUp);

        //Delete Slider 
        Task<bool> DeleteSlider(int sliderId);

        #endregion
    }
}
