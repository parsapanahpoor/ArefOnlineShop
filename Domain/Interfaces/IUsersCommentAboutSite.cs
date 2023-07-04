#region Admin Side 

using Domain.Models.UserCommentAboutSite;
using Domain.ViewModels.Admin.UsersCommentAboutSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.Interfaces
{
    public interface IUsersCommentAboutSiteRepository
    {
        #region Admin Side 

        //Fill Filter User Comment About Site Admin Side View Model
        Task<List<FilterUserCommentAboutSiteAdminSideViewModel>?> FilterUserCommentAboutSiteAdminSideViewModel();

        //Add Users Comments To The Data Base 
        Task AddUsersCommentsToTheDataBase(UsersCommentsAboutSite model);

        //Get Users Comment By Id
        Task<UsersCommentsAboutSite> GetUsersCommentsById(int id);

        //Fill Edit Users Comments Admin Side View Model
        Task<EditUsersCommentsAdminSideViewModel> FillEditUsersCommentsAdminSideViewModel(int id);

        //Update User Comment 
        Task UpdateUserComment(UsersCommentsAboutSite model);

        #endregion
    }
}
