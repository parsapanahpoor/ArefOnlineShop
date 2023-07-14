#region Using

using Domain.Models.ContactUs;
using Domain.ViewModels.Admin.UsersCommentAboutSite;
using Domain.ViewModels.SiteSide.ContactUs;
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

        //List Of Contact Us Requests
        Task<List<ContactUs>> ListOfContactUsRequests();

        //Get Contact Us With Id
        Task<ContactUs> GetContactUsWithId(int id);

        #endregion

        #region Site Side 

        Task<bool> AddContactUs(ContactUsSiteSideViewModel model);

        #endregion
    }
}
