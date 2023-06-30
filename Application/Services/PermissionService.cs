using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models.Permissions;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PermissionService : IPermissionService
    {

        private IPermissionRepository _permissionRepository;
        private IUserRepository _userRepository;

        public PermissionService(IPermissionRepository permissionRepository, IUserRepository userRepository)
        {
            _permissionRepository = permissionRepository;
            _userRepository = userRepository;
        }

        public void AddPermissionsToRole(int roleId, List<int> permission)
        {
            _permissionRepository.AddPermissionsToRole(roleId, permission);
        }

        public int AddRole(Role role)
        {
            return _permissionRepository.AddRole(role);
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            _permissionRepository.AddRolesToUser(roleIds, userId);
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            int userid = _userRepository.GetUserIdByUserName(userName);
            List<int> UserRoles = _permissionRepository.GetUserRoles(userid);

            if (!UserRoles.Any())
                return false;

            List<int> RolesPermission = _permissionRepository.GetRolesPermission(permissionId);

            return RolesPermission.Any(p => UserRoles.Contains(p));
        }

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            UpdateRole(role);
            Savechanges();
        }

        public void EditRolesUser(int userId, List<int> rolesId)
        {
            _permissionRepository.EditRolesUser(userId, rolesId);
        }

        public List<Permission> GetAllPermission()
        {
            return _permissionRepository.GetAllPermission();
        }

        public Role GetRoleById(int roleId)
        {
            return _permissionRepository.GetRoleById(roleId);
        }

        public List<Role> GetRoles()
        {
            return _permissionRepository.GetRoles();
        }

        public List<int> PermissionsRole(int roleId)
        {
            return _permissionRepository.PermissionsRole(roleId);
        }

        public void Savechanges()
        {
            _permissionRepository.Savechanges();
        }

        public void UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            _permissionRepository.UpdatePermissionsRole(roleId, permissions);
        }

        public void UpdateRole(Role role)
        {
            _permissionRepository.UpdateRole(role);
            Savechanges();
        }
    }
}
