#region Usings

using Domain.Models.Discount;
using Domain.ViewModels.Admin.DiscountCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.Interfaces
{
    public interface IDiscountCodeRepository
    {
        #region Admin Side 

        //Fill List Of Discount Code Admin Side View Model
        Task<List<ListOfDiscountCodeAdminSideViewModel>> FillListOfDiscountCodeAdminSideViewModel();

        //Add To The Data Base 
        Task AddToTheDataBase(DiscountCode model);

        //Get Discount Code By Id
        Task<DiscountCode> GetDiscountCodeById(int id);

        //Edit Discount Code 
        Task EditDiscountCode(DiscountCode discount);

        //Fill List Of Discount Selected From User Admin Side View Model
        Task<List<ListOfDiscountSelectedFromUserAdminSideViewModel>> FillListOfDiscountSelectedFromUserAdminSideViewModel(int id);

        #endregion
    }
}
