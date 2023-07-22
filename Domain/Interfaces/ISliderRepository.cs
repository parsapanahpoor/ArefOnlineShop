#region Using

using Domain.Models.Slider;
using Domain.ViewModels.Admin.Slider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.Interfaces
{
    public interface ISliderRepository
    {
        #region Admin Panel

        //Fill List Of Sliers Admin Side View Model
        Task<List<ListOfSliersAdminSideViewModel>?> FillListOfSliersAdminSideViewModel();

        //Add To The Data Base 
        Task AddToTheDataBase(Slider slider);

        //Fill Edit Slider View Model
        Task<EditSliderViewModel?> FillEditSliderViewModel(int sliderId);

        //Get Sldier By Id
        Task<Slider?> GetSldierById(int sliderId);

        //Update Slider Method 
        Task UpdateSliderMethod(Slider slider);

        #endregion

        #region Site Side 

        Task<List<Slider>> GetListOfSlidersForShowInLanding();

        #endregion
    }
}
