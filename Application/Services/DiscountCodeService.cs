#region Using

using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models.Discount;
using Domain.ViewModels.Admin.DiscountCode;
using Microsoft.AspNetCore.Razor.Language;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
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

        public DiscountCodeService(IDiscountCodeRepository discountCode)
        {
            _discountCodeRepository = discountCode;
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
    }
}
