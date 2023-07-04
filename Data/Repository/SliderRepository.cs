#region Using

using Data.Context;
using Domain.Interfaces;
using Domain.Models.Slider;
using Domain.ViewModels.Admin.Slider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Data.Repository
{
    public class SliderRepository : ISliderRepository
    {
        #region Ctor

        private readonly ParsaWorkShopContext _context;

        public SliderRepository(ParsaWorkShopContext context)
        {
            _context = context;
        }

        #endregion

        #region Admin Side 

        //Fill List Of Sliers Admin Side View Model
        public async Task<List<ListOfSliersAdminSideViewModel>?> FillListOfSliersAdminSideViewModel()
        {
            return await _context.Slider
                                 .AsNoTracking()
                                 .Where(p=> !p.IsDelete)
                                 .Select(p=> new ListOfSliersAdminSideViewModel()
                                 {
                                     SliderId = p.SliderId,
                                     SliderImage = p.SliderImageName,
                                     CreateDate = p.CreateDate,
                                     ColorCode = p.ColorCode,
                                 }).ToListAsync();
        }

        //Add To The Data Base 
        public async Task AddToTheDataBase(Slider slider)
        {
            await _context.Slider.AddAsync(slider);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
