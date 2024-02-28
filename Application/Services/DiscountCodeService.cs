#region Using

using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models.Discount;
using Domain.Models.Order;
using Domain.ViewModels.Admin.DiscountCode;
using Microsoft.AspNetCore.Razor.Language;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Application.Services
{
    public class DiscountCodeService : IDiscountCodeService
    {
        #region Ctor 

        private readonly IDiscountCodeRepository _discountCodeRepository;
        private readonly IOrderService _orderService;

        public DiscountCodeService(IDiscountCodeRepository discountCode, IOrderService orderService)
        {
            _discountCodeRepository = discountCode;
            _orderService = orderService;

        }

        #endregion

        #region Admin Side 

        //Fill List Of Discount Code Admin Side View Model
        public async Task<List<ListOfDiscountCodeAdminSideViewModel>> FillListOfDiscountCodeAdminSideViewModel()
        {
            return await _discountCodeRepository.FillListOfDiscountCodeAdminSideViewModel();
        }

        //Add Discount Code Admin  Side
        public async Task<bool> AddDiscountCodeAdminSide(CreateDiscoutCodeAdminSideViewModel model)
        {
            #region Fill Entity

            var code = $"Aref{new Random().Next(10000, 999999)}";

            DiscountCode discountCode = new DiscountCode()
            {
                Code = code,
                CreateDate = DateTime.Now,
                DiscountPercentage = model.DiscountPercentage,
                DiscountTitle = model.DiscountTitle,
                IsDelete = false,
            };

            //Add To The Data Base
            await _discountCodeRepository.AddToTheDataBase(discountCode);

            #endregion

            return true;
        }

        //Fill Edit Discout Code Admin Side View Model
        public async Task<EditDiscoutCodeAdminSideViewModel> FillEditDiscoutCodeAdminSideViewModel(int id)
        {
            #region Get Discount By Id 

            var discountCode = await _discountCodeRepository.GetDiscountCodeById(id);
            if (discountCode == null) return null;

            #endregion

            return new EditDiscoutCodeAdminSideViewModel()
            {
                DiscountPercentage = discountCode.DiscountPercentage,
                DiscountTitle = discountCode.DiscountTitle,
                Id = id,
            };
        }

        //Edit Discount Code 
        public async Task<bool> EditDiscountCode(EditDiscoutCodeAdminSideViewModel model)
        {
            #region Get Discount By Id 

            var discountCode = await _discountCodeRepository.GetDiscountCodeById(model.Id);
            if (discountCode == null) return false;

            #endregion

            #region Update 

            discountCode.DiscountPercentage = model.DiscountPercentage;
            discountCode.DiscountTitle = model.DiscountTitle;

            //Data Base Change
            await _discountCodeRepository.EditDiscountCode(discountCode);

            #endregion

            return true;
        }

        //Delete Discount Code
        public async Task<bool> DeleteDiscountCode(int id)
        {
            #region Get Discount By Id 

            var discountCode = await _discountCodeRepository.GetDiscountCodeById(id);
            if (discountCode == null) return false;

            #endregion

            #region Update 

            discountCode.IsDelete = true;

            //Data Base Change
            await _discountCodeRepository.EditDiscountCode(discountCode);

            #endregion

            return true;
        }

        //Fill List Of Discount Selected From User Admin Side View Model
        public async Task<List<ListOfDiscountSelectedFromUserAdminSideViewModel>> FillListOfDiscountSelectedFromUserAdminSideViewModel(int id)
        {
            return await _discountCodeRepository.FillListOfDiscountSelectedFromUserAdminSideViewModel(id);
        }

        #endregion

        #region Site Side 

        //Add Discount To The Order
        public async Task<int?> AddDiscountToTheOrder(int orderId, int userId, string discountName )
        {
            #region Get Discount By Discount Code 

            var discount = await _discountCodeRepository.GetDiscontCodeByDiscountName(discountName);
            if (discount == null) return null;

            #endregion

            #region Check 

            var order = await _discountCodeRepository.GetOerderByOrderIDAndUserID(orderId, userId);
            if (order == null) return null;

            #endregion

            #region Check Validation 

            var check = await _discountCodeRepository.CheckIsExistAnyUserSelectedDiscountByOrderAndUserAndDiscountID(orderId , userId , discount.Id);
            if (check == true) return null;

            #endregion

            #region Add Discount To The Order And Discount Selected User

            #region Discount Selected User

            DiscountCodeSelectedFromUser userSelected = new DiscountCodeSelectedFromUser()
            {
                CreateDate = DateTime.Now,
                DiscountId = discount.Id,
                IsDelete = false,
                UserId = userId,
                OrderId = order.OrderId
            };

            //Add To The Data Base 
            await _discountCodeRepository.AddDiscountSelectedUserToTheDataBase(userSelected);

            #endregion

            #endregion

            #region Initial Order Price After Discount

            List<OrderDetails> orderDetails = _orderService.GetAllOrderDetailsByOrderID(order.OrderId);

            int Amount = 0;

            foreach (var item in orderDetails)
            {
                Amount = Amount + (int)(item.Price * item.Count);
            }

            //Initial Disacount
            Amount = (Amount  * discount.DiscountPercentage) / 100;

            #endregion

            #region Order

            order.DiscountUserSelected = userSelected.Id;

            order.Price = Amount;

            //Update Order
            await _discountCodeRepository.UpdateOrder(order);

            #endregion

            return Amount;
        }

        //Add Discount To The Order Price
        public async Task<int> AddDiscountToTheOrderPrice(int discountSelectedUserId , int amount)
        {
            #region Get Discount Percentage

            int percentage = await _discountCodeRepository.GetDiscountPercentageWithUserSelectedDiscount(discountSelectedUserId);
            if (percentage == 0) return 0;

            #endregion

            #region Initial amount

            //Initial Disacount
            amount = (amount * percentage) / 100;

            #endregion

            return amount;
        }

        //Update Order
        public async Task UpdateOrder(Orders order)
        {
            await _discountCodeRepository.UpdateOrder(order);
        }

        #endregion
    }
}
