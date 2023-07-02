using Application.Interfaces;
using Application.Security;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    //[PermissionChecker(1)]

    public class UsersController : Controller
    {
        #region Ctor

        private IUserService _userService;
        private IPermissionService _permissionService;

        public UsersController(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        #endregion
     
        public IActionResult Index(int? id, bool Create = false, bool Edit = false, bool Delete = false)
        {
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;
            if (id != null)
            {
                var user = _userService.GetUsersInRoles((int)id);

                return View(user);
            }
            var users = _userService.GetUsers();

            return View(users);
        }

        public IActionResult DeletedUsers()
        {
            var users = _userService.GetDeleteUsers();
            return View(users);
        }

        public IActionResult Create()
        {
            ViewData["Roles"] = _permissionService.GetRoles();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserViewModel user, List<int> SelectedRoles)
        {
            if (ModelState.IsValid)
            {
                int userId = _userService.AddUserFromAdmin(user);
                _permissionService.AddRolesToUser(SelectedRoles, userId);

                return Redirect("/Admin/Users/Index?Create=true");
            }
            return View(user);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditUserViewModel user = _userService.GetUserForShowInEditMode((int)id);
            ViewData["Roles"] = _permissionService.GetRoles();

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditUserViewModel user = _userService.GetUserForShowInEditMode((int)id);
            ViewData["Roles"] = _permissionService.GetRoles();

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUserViewModel user, List<int> SelectedRoles)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.EditUserFromAdmin(user);
                    _permissionService.EditRolesUser(user.UserId, SelectedRoles);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_userService.IsExistUserName(user.UserName))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Admin/Users/Index?Edit=true");
            }
            ViewData["Roles"] = _permissionService.GetRoles();

            return View(user);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditUserViewModel user = _userService.GetUserForShowInEditMode((int)id);
            ViewData["Roles"] = _permissionService.GetRoles();
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _userService.GetUserById(id);
            _userService.DeleteUser(user.UserId);

            return Redirect("/Admin/Users/Index?Delete=true");
        }

        public IActionResult LockUser(int Userid, int id)
        {
            var user = _userService.GetUserById(Userid);

            if (id == 1)
            {
                user.IsActive = false;

            }
            if (id == 2)
            {
                user.IsActive = true;
            }
            _userService.UpdateUser(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
