﻿#region Using

using Domain.ViewModels.Admin.DiscountCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Application.Interfaces
{
    public interface IDiscountCodeService 
    {
        #region Admin Side 

        //Fill List Of Discount Code Admin Side View Model
        Task<List<ListOfDiscountCodeAdminSideViewModel>> FillListOfDiscountCodeAdminSideViewModel();

        //Add Discount Code Admin  Side
        Task<bool> AddDiscountCodeAdminSide(CreateDiscoutCodeAdminSideViewModel model);

        //Fill Edit Discout Code Admin Side View Model
        Task<EditDiscoutCodeAdminSideViewModel> FillEditDiscoutCodeAdminSideViewModel(int id);

        //Edit Discount Code 
        Task<bool> EditDiscountCode(EditDiscoutCodeAdminSideViewModel model);

        //Delete Discount Code
        Task<bool> DeleteDiscountCode(int id);

        //Fill List Of Discount Selected From User Admin Side View Model
        Task<List<ListOfDiscountSelectedFromUserAdminSideViewModel>> FillListOfDiscountSelectedFromUserAdminSideViewModel(int id);

        #endregion
    }
}
