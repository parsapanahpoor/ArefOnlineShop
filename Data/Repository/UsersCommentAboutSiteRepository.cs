#region Using

using Data.Context;
using Domain.Interfaces;
using Domain.Models.ContactUs;
using Domain.Models.UserCommentAboutSite;
using Domain.Models.Users;
using Domain.ViewModels.Admin.UsersCommentAboutSite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Data.Repository
{
    public class UsersCommentAboutSiteRepository : IUsersCommentAboutSiteRepository
    {
        #region Ctor

        private readonly ParsaWorkShopContext _context;

        public UsersCommentAboutSiteRepository(ParsaWorkShopContext context)
        {
            _context = context;
        }

        #endregion

        #region Admin Side 

        //Fill Filter User Comment About Site Admin Side View Model
        public async Task<List<FilterUserCommentAboutSiteAdminSideViewModel>?> FilterUserCommentAboutSiteAdminSideViewModel()
        {
            return await _context.UsersCommentsAboutSites
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete)
                                 .Select(p => new FilterUserCommentAboutSiteAdminSideViewModel()
                                 {
                                     CommentText = p.CommentText,
                                     Username = p.Username,
                                     Id = p.Id
                                 })
                                 .ToListAsync();
        }

        //Add Users Comments To The Data Base 
        public async Task AddUsersCommentsToTheDataBase(UsersCommentsAboutSite model)
        {
            await _context.UsersCommentsAboutSites.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        //Get Users Comment By Id
        public async Task<UsersCommentsAboutSite> GetUsersCommentsById(int id)
        {
            return await _context.UsersCommentsAboutSites
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.Id == id)
                                 .FirstOrDefaultAsync();
        }

        //Fill Edit Users Comments Admin Side View Model
        public async Task<EditUsersCommentsAdminSideViewModel> FillEditUsersCommentsAdminSideViewModel(int id)
        {
            return await _context.UsersCommentsAboutSites
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.Id == id)
                                 .Select(p => new EditUsersCommentsAdminSideViewModel()
                                 {
                                     Id = p.Id,
                                     UserComment = p.CommentText,
                                     Username = p.Username,
                                 })
                                 .FirstOrDefaultAsync();
        }

        //Update User Comment 
        public async Task UpdateUserComment(UsersCommentsAboutSite model)
        {
            _context.UsersCommentsAboutSites.Update(model);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Site Side 

        //Add Contact Us 
        public async Task AddContactUs(ContactUs contactUs)
        {
            await _context.ContactUs.AddAsync(contactUs);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
