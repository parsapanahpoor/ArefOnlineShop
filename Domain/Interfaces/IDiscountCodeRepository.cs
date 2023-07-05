#region Usings

using Domain.Models.Discount;
using Domain.Models.Order;
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

        //Get Discont Code By Discount Name
        Task<DiscountCode> GetDiscontCodeByDiscountName(string discountName);

        //Get Oerder By Order ID And User ID
        Task<Orders> GetOerderByOrderIDAndUserID(int ordersId, int userId);

        #endregion

        #region Site Side 

        //Check Is Exist Any User Selected Discount By Order And User And Discount ID
        Task<bool> CheckIsExistAnyUserSelectedDiscountByOrderAndUserAndDiscountID(int orderId, int userId, int discountId);

        //Add Discount Selected User To The Data Base 
        Task AddDiscountSelectedUserToTheDataBase(DiscountCodeSelectedFromUser selectedUser);

        //Update Order
        Task UpdateOrder(Orders order);

        //Get Discount Percentage With User Selected Discount 
        Task<int> GetDiscountPercentageWithUserSelectedDiscount(int userSelectedDiscountId);

        #endregion
    }
}
