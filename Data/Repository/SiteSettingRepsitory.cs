#region Using

using Data.Context;
using Domain.Interfaces;
using Domain.Models.Product;
using Domain.ViewModels.Admin.SiteSetting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Data.Repository
{
    public class SiteSettingRepsitory : ISiteSettingRepsitory
    {
        #region Ctor

        private readonly ParsaWorkShopContext _context;

        public SiteSettingRepsitory(ParsaWorkShopContext context)
        {
            _context = context;
        }

        #endregion

        #region Admin Side 

        #region Product Color

        //Fill List Of Product Colors Admin Side View Model
        public async Task<List<ListOfProductColorsAdminSideViewModel>> FillListOfProductColorsAdminSideViewModel()
        {
            return await _context.ProductColors
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete)
                                 .Select(p => new ListOfProductColorsAdminSideViewModel()
                                 {
                                     Id = p.Id,
                                     ProductColorImage = p.ColorImage,
                                     ProductName = p.ColorTitle
                                 })
                                 .ToListAsync();
        }

        //Add Color To The Data Base
        public async Task AddColorToTheDataBase(ProductColor color)
        {
            await _context.ProductColors.AddAsync(color);
            await _context.SaveChangesAsync();
        }

        //Fill Edit Color Admin Side View Model
        public async Task<EditColorAdminSideViewModel> FillEditColorAdminSideViewModel(int id)
        {
            return await _context.ProductColors
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.Id == id)
                                 .Select(p => new EditColorAdminSideViewModel()
                                 {
                                     ColorId = p.Id,
                                     ColorImageName = p.ColorImage,
                                     ColorName = p.ColorTitle
                                 })
                                 .FirstOrDefaultAsync();
        }

        //Get Color By Id 
        public async Task<ProductColor> GetColorById(int colorId)
        {
            return await _context.ProductColors
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(p => !p.IsDelete && p.Id == colorId);
        }

        //Update Color 
        public async Task UpdateColor(ProductColor color)
        {
            _context.ProductColors.Update(color);
            await _context.SaveChangesAsync();
        }

        #endregion

        #endregion
    }

}
