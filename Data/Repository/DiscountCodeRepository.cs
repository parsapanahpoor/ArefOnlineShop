﻿#region Using

using Data.Context;
using Domain.Interfaces;
using Domain.Models.Discount;
using Domain.ViewModels.Admin.DiscountCode;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Data.Repository
{
    public class DiscountCodeRepository : IDiscountCodeRepository
    {
        #region Ctor 

        private readonly ParsaWorkShopContext _context;

        public DiscountCodeRepository(ParsaWorkShopContext context)
        {
                _context = context;
        }

        #endregion

        #region Admin Side 

        //Fill List Of Discount Code Admin Side View Model
        public async Task<List<ListOfDiscountCodeAdminSideViewModel>> FillListOfDiscountCodeAdminSideViewModel()
        {
            return await _context.DiscountCodes
                                 .AsNoTracking()
                                 .Where(p=> !p.IsDelete)
                                 .Select(p=> new ListOfDiscountCodeAdminSideViewModel()
                                 {
                                     Code = p.Code,
                                     DiscountPercentage = p.DiscountPercentage,
                                     DiscountTitle = p.DiscountTitle,
                                     Id = p.Id,
                                     CountOfUsage = _context.DiscountCodeSelectedUsers
                                                            .AsNoTracking()
                                                            .Where (s=> !s.IsDelete && s.DiscountId == p.Id)
                                                            .Count()
                                 })
                                 .ToListAsync();
        }

        //Add To The Data Base 
        public async Task AddToTheDataBase(DiscountCode model)
        {
            await _context.DiscountCodes.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        //Get Discount Code By Id
        public async Task<DiscountCode> GetDiscountCodeById(int id)
        {
            return await _context.DiscountCodes
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(p=> !p.IsDelete && p.Id == id);
        }

        //Edit Discount Code 
        public async Task EditDiscountCode(DiscountCode discount)
        {
            _context.DiscountCodes.Update(discount);
            await _context.SaveChangesAsync();
        }

        //Fill List Of Discount Selected From User Admin Side View Model
        public async Task<List<ListOfDiscountSelectedFromUserAdminSideViewModel>> FillListOfDiscountSelectedFromUserAdminSideViewModel(int id)
        {
            return await _context.DiscountCodeSelectedUsers
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.DiscountId == id)
                                 .Select(p => new ListOfDiscountSelectedFromUserAdminSideViewModel()
                                 {
                                     CreateDate = p.CreateDate,
                                     DiscountId = p.Id,
                                     OrdeId = p.OrderId,
                                     User = _context.Users
                                                    .AsNoTracking()
                                                    .Where(s=> !s.IsDelete && s.UserId == p.UserId)
                                                    .Select(s=> new UserSelectedDiscount()
                                                    {
                                                        Mobile = s.PhoneNumber,
                                                        UserId = s.UserId,
                                                        UserAvatar = s.UserAvatar,
                                                        Username = s.UserName
                                                    })
                                                    .FirstOrDefault()
                                 })
                                 .OrderByDescending(p => p.CreateDate)
                                 .ToListAsync();
        }

        #endregion
    }
}
