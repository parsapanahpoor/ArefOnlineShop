using Application.Interfaces;
using Application.Security;
using Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [PermissionChecker(1)]

    public class RolesController : Controller
    {
        #region Ctor

        private IPermissionService _permissionService;

        public RolesController(IPermissionService permission)
        {
            _permissionService = permission;
        }

        #endregion

        public IActionResult Index(bool Create = false, bool Edit = false, bool Delete = false)
        {
            var role = _permissionService.GetRoles();
            ViewBag.Create = Create;
            ViewBag.Edit = Edit;
            ViewBag.Delete = Delete;

            return View(role);
        }

        public IActionResult Create()
        {
            ViewData["Permissions"] = _permissionService.GetAllPermission();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("RoleId,RoleTitle,IsDelete")] Role role, List<int> SelectedPermission)
        {
            if (ModelState.IsValid)
            {

                role.IsDelete = false;
                int roleId = _permissionService.AddRole(role);

                _permissionService.AddPermissionsToRole(roleId, SelectedPermission);



                return Redirect("/Admin/Roles/Index?Create=true");
            }
            return View(role);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _permissionService.GetRoleById((int)id);

            if (role == null)
            {
                return NotFound();
            }
            ViewData["Permissions"] = _permissionService.GetAllPermission();
            ViewData["SelectedPermissions"] = _permissionService.PermissionsRole((int)id);
            return View(role);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("RoleId,RoleTitle,IsDelete")] Role role, List<int> SelectedPermission)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _permissionService.UpdateRole(role);

                _permissionService.UpdatePermissionsRole(role.RoleId, SelectedPermission);



                return Redirect("/Admin/Roles/Index?Edit=true");
            }
            return View(role);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Permissions"] = _permissionService.GetAllPermission();
            ViewData["SelectedPermissions"] = _permissionService.PermissionsRole((int)id);
            var role = _permissionService.GetRoleById((int)id);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var role = _permissionService.GetRoleById((int)id);

            _permissionService.DeleteRole(role);

            return Redirect("/Admin/Roles/Index?Delete=true");
        }
    }
}
