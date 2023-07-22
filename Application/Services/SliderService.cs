#region Using

using AngleSharp.Html;
using Application.Extensions;
using Application.Interfaces;
using Application.StaticTools;
using Domain.Interfaces;
using Domain.Models.Slider;
using Domain.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
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
        public async Task<bool> AddSliderAdminSide(CreateSliderAdminSideViewModel slider, IFormFile sliderImage)
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
                Priority = slider.Priority,
                LinkTitle = slider.LinkTitle,
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

        //Fill Edit Slider View Model
        public async Task<EditSliderViewModel?> FillEditSliderViewModel(int sliderId)
        {
            return await _sliderRepository.FillEditSliderViewModel(sliderId);
        }

        //Edit Slider Admin Side 
        public async Task<bool> EditSliderAdminSidel(EditSliderViewModel model, IFormFile? imgBlogUp)
        {
            #region Get Slider By Id 

            var slider = await _sliderRepository.GetSldierById(model.SliderId);
            if (slider == null) { return false; }

            #endregion

            #region Edit Slider Fields

            slider.FirstText = model.FirstText;
            slider.SecondeText = model.SecondeText;
            slider.ThirdText = model.ThirdText;
            slider.Link = model.Link;
            slider.ColorCode = model.ColorCode;
            slider.Priority = model.Priority;
            slider.LinkTitle = model.LinkTitle;

            #region Update Slider Image

            if (imgBlogUp != null)
            {
                var imageName = Guid.NewGuid() + Path.GetExtension(imgBlogUp.FileName);
                imgBlogUp.AddImageToServer(imageName, PathTools.SliderPathServer, 400, 300, PathTools.SliderPathThumbServer);

                if (!string.IsNullOrEmpty(slider.SliderImageName))
                {
                    slider.SliderImageName.DeleteImage(PathTools.SliderPathServer, PathTools.SliderPathThumbServer);
                }

                slider.SliderImageName = imageName;
            }

            #endregion

            #endregion

            #region Update Method

            await _sliderRepository.UpdateSliderMethod(slider);

            #endregion

            return true;
        }

        //Delete Slider 
        public async Task<bool> DeleteSlider(int sliderId)
        {
            #region Get Slider By Id 

            var slider = await _sliderRepository.GetSldierById(sliderId);
            if (slider == null) { return false; }

            #endregion

            #region Delete Slider 

            slider.IsDelete = true;


            if (!string.IsNullOrEmpty(slider.SliderImageName))
            {
                slider.SliderImageName.DeleteImage(PathTools.SliderPathServer, PathTools.SliderPathThumbServer);
            }

            #endregion

            #region Update Method

            await _sliderRepository.UpdateSliderMethod(slider);

            #endregion

            return true;
        }

        #endregion

        #region Site Side 

        public async Task<List<Slider>> GetListOfSlidersForShowInLanding()
        {
            return await _sliderRepository.GetListOfSlidersForShowInLanding();
        }

        #endregion
    }
}
