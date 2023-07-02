using Data.Context;
using Domain.Interfaces;
using Domain.Models.ContactUs;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Ctor

        private ParsaWorkShopContext _context;

        public UserRepository(ParsaWorkShopContext context)
        {
            _context = context;
        }

        #endregion

        #region Old Versions

        public void addMessage(ContactUs contactus)
        {
            _context.ContactUs.Add(contactus);
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public bool CompareOldPassword(string hashOldPassword, string username)
        {
            return _context.Users.Any(u => u.UserName == username && u.Password == hashOldPassword);
        }

        public List<ContactUs> GetAllMessages()
        {
            return _context.ContactUs.ToList();
        }

        public List<User> GetDeleteUsers()
        {
            return _context.Users.IgnoreQueryFilters().Where(p => p.IsDelete).ToList();
        }

        public List<User> GetLAstestUserForAdminIndexPage()
        {
            return _context.Users.OrderByDescending(p => p.RegisterDate).Take(4).ToList();
        }

        public ContactUs GetMessageById(int id)
        {
            return _context.ContactUs.Find(id);
        }

        public User GetUserByActiveCode(string ActiveCode)
        {
            return _context.Users.FirstOrDefault(p => p.MobileActivationCode == ActiveCode);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserById(int Userid)
        {
            return _context.Users.Find(Userid);
        }

        public async Task<User> GetUserByIdAsync(int Userid)
        {
            return await _context.Users.FindAsync(Userid);
        }

        public User GetUserByPhoneNumber(string PhoneNumber)
        {
            return _context.Users.FirstOrDefault(p => p.PhoneNumber == PhoneNumber);
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == username);
        }

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName).UserId;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public List<User> GetUsersInRoles(int Role)
        {
            var userid = _context.UsersRoles.Where(p => p.RoleId == Role).Select(p => p.UserId).ToList();
            var users = _context.Users.Where(p => userid.Contains(p.UserId)).ToList();
            return users;
        }

        public List<int> GetUsersRoles(User user)
        {
            return _context.UsersRoles.Where(p => p.UserId == user.UserId).Select(p => p.RoleId).ToList();
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsExistPhoneNumber(string PhoneNumber)
        {
            return _context.Users.Any(p => p.PhoneNumber == PhoneNumber);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public User LoginUser(string PhoneNumber, string Password)
        {
            return _context.Users.SingleOrDefault(u => u.PhoneNumber == PhoneNumber && u.Password == Password);
        }

        public void Savechanges()
        {
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
        }

        #endregion

        #region Generals

        public async Task<bool> IsExistUserById(int userId)
        {
            return await _context.Users.AnyAsync(p => !p.IsDelete && p.UserId == userId);
        }

        #endregion
    }
}
