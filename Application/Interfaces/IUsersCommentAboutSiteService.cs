﻿#region Using

using Domain.ViewModels.Admin.UsersCommentAboutSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Application.Interfaces
{
    public interface IUsersCommentAboutSiteService
    {
        #region Admin Side

        //Fill Filter User Comment About Site Admin Side View Model
        Task<List<FilterUserCommentAboutSiteAdminSideViewModel>?> FilterUserCommentAboutSiteAdminSideViewModel();


        //Create Users Comment Admin Side
        Task<bool> CreateUserComment(CreateUsersCommentsAdminSideViewModel model);

        //Fill EditUsersCommentsAdminSideViewModel
        Task<EditUsersCommentsAdminSideViewModel> FillEditUsersCommentsAdminSideViewModel(int id);

        //Edit User Comment 
        Task<bool> EditUserComment(EditUsersCommentsAdminSideViewModel model);

        //Delete Users Comment 
        Task<bool> DeleteUsersComment(int id);

        #endregion
    }
}
