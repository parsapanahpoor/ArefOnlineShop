#region Using

using Application.Extensions;
using Application.Interfaces;
using Application.StaticTools;
using Domain.Interfaces;
using Domain.Models.Slider;
using Domain.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Application.Services
{
    public class SliderService : ISliderService
    {
        #region Ctor

        private readonly ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        #endregion

        #region Admin Panel

        //Fill List Of Sliers Admin Side View Model
        public async Task<List<ListOfSliersAdminSideViewModel>?> FillListOfSliersAdminSideViewModel()
        {
            return await _sliderRepository.FillListOfSliersAdminSideViewModel();
        }

        //Add Slider Admin Side
        public async Task<bool> AddSliderAdminSide(CreateSliderAdminSideViewModel slider , IFormFile sliderImage)
        {
            #region Fill Slider 

            Slider sliderEntity = new Slider()
            {
                ColorCode = slider.ColorCode,
                CreateDate = DateTime.Now,
                EndDatetDate = DateTime.Now,
                FirstText = slider.FirstText,
                IsActive = true,
                IsDelete = false,
                Link = slider.Link,
                SecondeText = slider.SecondeText,
                StartDate = DateTime.Now,
                ThirdText = slider.ThirdText,
            };

            #endregion

            #region Add Slider Image

            if (sliderImage != null)
            {
                var imageName = Guid.NewGuid() + Path.GetExtension(sliderImage.FileName);
                sliderImage.AddImageToServer(imageName, PathTools.SliderPathServer, 400, 300, PathTools.SliderPathThumbServer);

                sliderEntity.SliderImageName = imageName;
            }

            #endregion

            //Add To The Data Base
            await _sliderRepository.AddToTheDataBase(sliderEntity);

            return true;
        }

        #endregion
    }
}
