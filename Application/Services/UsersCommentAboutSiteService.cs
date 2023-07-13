#region Using

using Application.Interfaces;
using Data.Repository;
using Domain.Interfaces;
using Domain.Models.ContactUs;
using Domain.Models.UserCommentAboutSite;
using Domain.ViewModels.Admin.UsersCommentAboutSite;
using Domain.ViewModels.SiteSide.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Application.Services
{
    public class UsersCommentAboutSiteService : IUsersCommentAboutSiteService
    {
        #region Ctor 

        private readonly IUsersCommentAboutSiteRepository _usersCommentAboutSite;

        public UsersCommentAboutSiteService(IUsersCommentAboutSiteRepository usersCommentAboutSite)
        {
            _usersCommentAboutSite = usersCommentAboutSite;
        }

        #endregion

        #region Admin Side 

        //Fill Filter User Comment About Site Admin Side View Model
        public async Task<List<FilterUserCommentAboutSiteAdminSideViewModel>?> FilterUserCommentAboutSiteAdminSideViewModel()
        {
            return await _usersCommentAboutSite.FilterUserCommentAboutSiteAdminSideViewModel();
        }

        //Create Users Comment Admin Side
        public async Task<bool> CreateUserComment(CreateUsersCommentsAdminSideViewModel model)
        {
            #region Fill Entity

            UsersCommentsAboutSite userComment = new UsersCommentsAboutSite()
            {
                CommentText = model.CommentText,
                Username = model.Username
            };

            #endregion

            //Add To The Data Base
            await _usersCommentAboutSite.AddUsersCommentsToTheDataBase(userComment);

            return true;
        }

        //Fill EditUsersCommentsAdminSideViewModel
        public async Task<EditUsersCommentsAdminSideViewModel> FillEditUsersCommentsAdminSideViewModel(int id)
        {
            return await _usersCommentAboutSite.FillEditUsersCommentsAdminSideViewModel(id);
        }

        //Edit User Comment 
        public async Task<bool> EditUserComment(EditUsersCommentsAdminSideViewModel model)
        {
            #region Get User Comment 

            var origin = await _usersCommentAboutSite.GetUsersCommentsById(model.Id);
            if (origin == null) { return false; }

            #endregion

            #region Update Methods

            origin.Username = model.Username;
            origin.CommentText = model.UserComment;

            #endregion

            //Update Data Base
            await _usersCommentAboutSite.UpdateUserComment(origin);

            return true;
        }

        //Delete Users Comment 
        public async Task<bool> DeleteUsersComment(int id)
        {
            #region Get User Comment 

            var origin = await _usersCommentAboutSite.GetUsersCommentsById(id);
            if (origin == null) { return false; }

            #endregion

            #region Update Methods

            origin.IsDelete = true;

            #endregion

            //Update Data Base
            await _usersCommentAboutSite.UpdateUserComment(origin);

            return true;
        }

        #endregion

        #region Site Side 

        public async Task<bool> AddContactUs(ContactUsSiteSideViewModel model)
        {
            #region Fill Entity

            ContactUs contactUs = new ContactUs()
            {
                Email = model.Email,
                LongDescription = model.LongDescription,
                PhoneNumber = model.PhoneNumber,
                ShortDescription = model.ShortDescription,
                UserName = model.UserName
            };

            #endregion

            #region Add To The Data Base 

            await _usersCommentAboutSite.AddContactUs(contactUs);

            #endregion

            return true;
        }

        #endregion
    }
}
