using Application.Convertors;
using Application.Genarator;
using Application.Generators;
using Application.Interfaces;
using Application.Security;
using Application.StaticTools;
using Application.ViewModels;
using Application.ViewModels.SiteSide.Account;
using Data.Context;
using Domain.Interfaces;
using Domain.Models.ContactUs;
using Domain.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        #region Ctor

        private readonly IUserRepository _userRepository;
        private readonly ParsaWorkShopContext _context;
        private static readonly HttpClient client = new HttpClient();

        public UserService(IUserRepository userRepository, ParsaWorkShopContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        #endregion

        #region Old Version Codes

        public void addMessage(ContactUs contactus)
        {
            ContactUs contact = new ContactUs()
            {
                Email = contactus.Email,
                PhoneNumber = contactus.PhoneNumber,
                UserName = contactus.UserName,
                ShortDescription = contactus.ShortDescription,
                LongDescription = contactus.LongDescription
            };
            _userRepository.addMessage(contact);
            _userRepository.Savechanges();
        }

        public void AddUerByService(RegisterViewModel register)
        {
            User user = new User()
            {
                Email = FixedText.FixEmail("@yahoo.com"),
                IsActive = true,
                PhoneNumber = register.Mobile,
                Password = register.Password,
                RegisterDate = DateTime.Now,
                UserAvatar = "Defult.jpg",
                UserName = register.UserName
            };

            AddUser(user);
        }

        public int AddUser(User user)
        {
            return _userRepository.AddUser(user);
        }

        public int AddUserFromAdmin(CreateUserViewModel user)
        {
            User addUser = new User();
            addUser.Password = user.Password;
            addUser.PhoneNumber = user.PhoneNumber;
            addUser.Email = user.Email;
            addUser.IsActive = true;
            addUser.RegisterDate = DateTime.Now;
            addUser.UserName = user.UserName;
            addUser.IsDelete = false;

            #region Save Avatar

            if (user.UserAvatar != null)
            {
                string imagePath = "";
                addUser.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", addUser.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar/thumb", addUser.UserAvatar);

                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }

            #endregion

            return AddUser(addUser);
        }


        public void ChangeUserPassword(string userName, string newPassword)
        {
            var user = GetUserByUserName(userName);
            user.Password = newPassword;
            UpdateUser(user);
        }

        public bool CompareOldPassword(string oldPassword, string username)
        {
            string hashOldPassword = oldPassword;
            return _userRepository.CompareOldPassword(hashOldPassword, username);
        }

        public void DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            user.IsDelete = true;
            if (user.UserAvatar != "Defult.jpg")
            {
                string imagePath = "";
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", user.UserAvatar);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar/thumb", user.UserAvatar);
                if (File.Exists(deletethumbPath))
                {
                    File.Delete(deletethumbPath);
                }
            }
            user.UserAvatar = "Defult.jpg";

            UpdateUser(user);
        }

        public void EditProfile(string username, EditProfileViewModel profile)
        {
            if (profile.UserAvatar != null)
            {
                string imagePath = "";
                if (profile.AvatarName != "Defult.jpg")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", profile.AvatarName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar/thumb", profile.AvatarName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }

                profile.AvatarName = NameGenerator.GenerateUniqCode() + Path.GetExtension(profile.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", profile.AvatarName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    profile.UserAvatar.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar/thumb", profile.AvatarName);

                imgResizer.Image_resize(imagePath, thumbPath, 150);

            }

            User user = GetUserByUserName(username);
            user.Email = profile.Email;
            user.PhoneNumber = profile.PhoneNumber;
            user.UserAvatar = profile.AvatarName;

            UpdateUser(user);
        }

        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            User user = GetUserById(editUser.UserId);
            user.Email = editUser.Email;
            user.PhoneNumber = editUser.PhoneNumber;
            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = editUser.Password;
            }

            if (editUser.UserAvatar != null)
            {
                //Delete old Image
                if (editUser.AvatarName != "Defult.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", editUser.AvatarName);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar/thumb", editUser.AvatarName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }

                //Save New Image
                user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(editUser.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editUser.UserAvatar.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatar/thumb", user.UserAvatar);

                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }

            UpdateUser(user);
        }

        public List<ContactUs> GetAllMessages()
        {
            return _userRepository.GetAllMessages();
        }

        public EditProfileViewModel GetDataForEditProfileUser(string username)
        {
            User user = GetUserByUserName(username);

            EditProfileViewModel editProfile = new EditProfileViewModel()
            {
                AvatarName = user.UserAvatar,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };

            return editProfile;
        }

        public List<User> GetDeleteUsers()
        {
            return _userRepository.GetDeleteUsers();
        }

        public List<User> GetLastestUserForAdminPage()
        {
            return _userRepository.GetLAstestUserForAdminIndexPage();
        }

        public ContactUs GetMessageById(int id)
        {
            return _userRepository.GetMessageById(id);
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
        {
            User user = GetUserByUserName(username);

            SideBarUserPanelViewModel sideBarUser = new SideBarUserPanelViewModel()
            {
                UserName = user.UserName,
                ImageName = user.UserAvatar
            };

            return sideBarUser;
        }

        public User GetUserByActiveCode(string ActiveCode)
        {
            return _userRepository.GetUserByActiveCode(ActiveCode);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email.Trim().ToLower());
        }

        public User GetUserById(int Userid)
        {
            return _userRepository.GetUserById(Userid);
        }

        public async Task<User> GetUserByIdAsync(int Userid)
        {
            return await _userRepository.GetUserByIdAsync(Userid);
        }

        public User GetUserByPhoneNumber(string PhoneNumber)
        {
            return _userRepository.GetUserByPhoneNumber(PhoneNumber.Trim().ToLower());
        }

        public User GetUserByUserName(string username)
        {
            return _userRepository.GetUserByUserName(username);
        }

        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
            User user = GetUserById(userId);
            List<int> roles = GetUsersRoles(user.UserName);

            EditUserViewModel editUser = new EditUserViewModel()
            {
                UserId = user.UserId,
                PhoneNumber = user.PhoneNumber,
                AvatarName = user.UserAvatar,
                Password = user.Password,
                Email = user.Email,
                UserName = user.UserName,
                UserRoles = roles
            };

            return editUser;
        }

        public int GetUserIdByUserName(string userName)
        {
            return _userRepository.GetUserIdByUserName(userName);
        }

        public InformationUserViewModel GetUserInformation(string username)
        {
            var user = GetUserByUserName(username);
            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.PhoneNumber = user.PhoneNumber;

            return information;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public List<User> GetUsersInRoles(int Role)
        {
            return _userRepository.GetUsersInRoles(Role);
        }

        public List<int> GetUsersRoles(string username)
        {
            User user = GetUserByUserName(username);
            return _userRepository.GetUsersRoles(user);
        }

        public bool IsExistEmail(string email)
        {
            return _userRepository.IsExistEmail(email.Trim().ToLower());
        }

        public bool IsExistPhoneNumber(string PhoneNumber)
        {
            return _userRepository.IsExistPhoneNumber(PhoneNumber);
        }

        public bool IsExistUserName(string userName)
        {
            return _userRepository.IsExistUserName(userName);
        }

        public User LoginUser(LoginViewModel login)
        {
            string PhoneNumber = FixedText.FixEmail(login.Mobile);
            return _userRepository.LoginUser(PhoneNumber, login.Password);
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
            _userRepository.Savechanges();
        }

        #endregion

        #region Site Side 

        public async Task<RegisterUserResult> RegisterUser(RegisterViewModel register)
        {
            #region Model State Validation 

            //Fix Email Format
            var mobile = register.Mobile.Trim().ToLower().SanitizeText();

            //Check Mobile Number
            if (await IsExistUserByMobile(register.Mobile))
            {
                return RegisterUserResult.MobileExist;
            }

            //Check Username
            if (await IsExistUserByUsername(register.UserName))
            {
                return RegisterUserResult.UsernameExist;
            }

            //Field about Accept Site Roles
            if (register.SiteRoles == false)
            {
                return RegisterUserResult.SiteRoleNotAccept;
            }

            //Hash Password
            var password = PasswordHasher.EncodePasswordMd5(register.Password.SanitizeText());

            #endregion

            #region Add User 

            //Create User
            var User = new User()
            {
                Email = new Random().Next(10000, 999999).ToString()+"@yahoo.com",
                Password = password,
                UserName = mobile,
                PhoneNumber = register.Mobile.SanitizeText(),
                EmailActivationCode = CodeGenerator.GenerateUniqCode(),
                MobileActivationCode = new Random().Next(10000, 999999).ToString(),
                ExpireMobileSMSDateTime = DateTime.Now,
                IsAdmin = false,
                UserAvatar = "Defult.jpg",
                RegisterDate = DateTime.Now,
            };

            await _context.Users.AddAsync(User);
            await _context.SaveChangesAsync();

            #endregion

            #region Send Verification Code SMS

            var result = $"https://api.kavenegar.com/v1/6A427559367558527A76485753667A5779587337736735753945747946474F347A346A65356E7A567A51413D/verify/lookup.json?receptor={User.PhoneNumber}&token={User.MobileActivationCode}&template=Register";
            var results = client.GetStringAsync(result);

            #endregion

            return RegisterUserResult.Success;
        }

        public async Task<bool> IsExistsUserByEmail(string email)
        {
            return await _context.Users.AnyAsync(s => s.Email == email.Trim().ToLower() && !s.IsDelete);
        }

        public async Task<bool> IsExistUserByMobile(string mobile)
        {
            return await _context.Users.AnyAsync(p => p.PhoneNumber == mobile && !p.IsDelete);
        }

        public async Task<bool> IsExistUserByUsername(string userName)
        {
            return await _context.Users.AnyAsync(p => p.UserName == userName && !p.IsDelete);
        }

        public async Task<User?> GetUserByMobile(string mobile)
        {
            return await _context.Users.FirstOrDefaultAsync(s =>
                s.PhoneNumber == mobile.Trim().ToLower() && !s.IsDelete);
        }

        public async Task ResendActivationCodeSMS(string Mobile)
        {
            var user = await GetUserByMobile(Mobile);
            user.MobileActivationCode = new Random().Next(10000, 999999).ToString();
            user.ExpireMobileSMSDateTime = DateTime.Now;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            #region Send Verification Code SMS

            var result = $"https://api.kavenegar.com/v1/6A427559367558527A76485753667A5779587337736735753945747946474F347A346A65356E7A567A51413D/verify/lookup.json?receptor={user.PhoneNumber}&token={user.MobileActivationCode}&template=Register";
            var results = client.GetStringAsync(result);

            //var message = Messages.SendActivationRegisterSms(user.MobileActivationCode);

            //await _smsservice.SendSimpleSMS(user.Mobile, message);

            #endregion

        }

        public async Task<LoginResult> CheckUserForLogin(LoginViewModel login)
        {
            // get email and password
            var password = PasswordHasher.EncodePasswordMd5(login.Password.SanitizeText());
            var mobile = login.Mobile.Trim().ToLower().SanitizeText();

            // get user by email
            var user = await GetUserByMobile(mobile);

            // check user exists
            if (user == null) return LoginResult.UserNotFound;

            // check user password
            if (!user.Password.Equals(password)) return LoginResult.UserNotFound;

            // check user ban status
            if (user.IsBan) return LoginResult.UserIsBan;

            // check user activation
            if (!user.IsMobileConfirm) return LoginResult.MobileNotActivated;

            return LoginResult.Success;
        }

        public async Task<ActiveMobileByActivationCodeResult> ActiveUserMobile(ActiveMobileByActivationCodeViewModel activeMobileByActivationCodeViewModel)
        {
            #region Get User By Mobile

            if (!await IsExistUserByMobile(activeMobileByActivationCodeViewModel.Mobile.SanitizeText()))
            {
                return ActiveMobileByActivationCodeResult.AccountNotFound;
            }

            var user = await GetUserByMobile(activeMobileByActivationCodeViewModel.Mobile.SanitizeText());
            if (user == null) return ActiveMobileByActivationCodeResult.AccountNotFound;

            #endregion

            #region Validation Activation Code

            if (user.MobileActivationCode != activeMobileByActivationCodeViewModel.MobileActiveCode)
            {
                return ActiveMobileByActivationCodeResult.AccountNotFound;
            }

            #endregion

            #region Update User 

            user.IsMobileConfirm = true;
            user.MobileActivationCode = new Random().Next(10000, 999999).ToString();

            await _context.SaveChangesAsync();

            #endregion

            return ActiveMobileByActivationCodeResult.Success;
        }

        public async Task<ForgotPasswordResult> RecoverUserPassword(ForgetPasswordViewModel forgot)
        {
            #region Get User By Mobile

            var user = await GetUserByMobile(forgot.Mobile);
            if (user == null) return ForgotPasswordResult.NotFound;

            if (user != null && user.IsBan)
            {
                return ForgotPasswordResult.UserIsBlocked;
            }

            #endregion

            #region Update User Info

            user.MobileActivationCode = new Random().Next(10000, 999999).ToString();
            user.ExpireMobileSMSDateTime = DateTime.Now;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            #endregion

            #region Send Verification Code SMS

            var result = $"https://api.kavenegar.com/v1/6A427559367558527A76485753667A5779587337736735753945747946474F347A346A65356E7A567A51413D/verify/lookup.json?receptor={user.PhoneNumber}&token={user.MobileActivationCode}&template=Register";
            var results = client.GetStringAsync(result);

            #endregion

            return ForgotPasswordResult.Success;
        }

        public async Task<ResetPasswordResult> ResetUserPassword(ResetPasswordViewModel pass, string mobile)
        {
            #region Get User By Mobile

            var user = await GetUserByMobile(mobile);
            if (user == null) return ResetPasswordResult.NotFound;

            if (user.MobileActivationCode != pass.ActiveCode) return ResetPasswordResult.WrongActiveCode;

            #endregion

            #region Update User Info

            user.Password = PasswordHasher.EncodePasswordMd5(pass.Password.SanitizeText());
            user.IsMobileConfirm = true;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            #endregion

            return ResetPasswordResult.Success;
        }

        #endregion
    }
}
