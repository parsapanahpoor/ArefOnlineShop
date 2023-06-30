using Data.Context;
using Domain.Interfaces;
using Domain.Models.Permissions;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        #region Ctor

        private ParsaWorkShopContext _context;

        public PermissionRepository(ParsaWorkShopContext context)
        {
            _context = context;
        }

        #endregion

        public void AddPermissionsToRole(int roleId, List<int> permission)
        {
            foreach (var p in permission)
            {
                _context.RolePermission.Add(new RolePermission()
                {
                    PermissionId = p,
                    RoleId = roleId
                });
            }

            Savechanges();
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            Savechanges();

            return role.RoleId;
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (int roleId in roleIds)
            {
                _context.UsersRoles.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }

            Savechanges();
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            throw new NotImplementedException();
        }

        public void EditRolesUser(int userId, List<int> rolesId)
        {
            //Delete All Roles User
            _context.UsersRoles.Where(r => r.UserId == userId).ToList().ForEach(r => _context.UsersRoles.Remove(r));

            //Add New Roles
            AddRolesToUser(rolesId, userId);
        }

        public List<Permission> GetAllPermission()
        {
            return _context.Permission.ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Single(p => p.RoleId == roleId);
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public List<int> GetRolesPermission(int permissionid)
        {
            return  _context.RolePermission
                            .Where(p => p.PermissionId == permissionid)
                            .Select(p => p.RoleId).ToList();
        }

        public List<int> GetUserRoles(int userid)
        {
            return _context.UsersRoles
                           .Where(r => r.UserId == userid).Select(r => r.RoleId).ToList();
        }

        public List<int> PermissionsRole(int roleId)
        {
            return _context.RolePermission
                            .Where(r => r.RoleId == roleId)
                            .Select(r => r.PermissionId).ToList();
        }

        public void Savechanges()
        {
            _context.SaveChanges();
        }

        public void UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            _context.RolePermission.Where(p => p.RoleId == roleId)
                            .ToList().ForEach(p => _context.RolePermission.Remove(p));

            AddPermissionsToRole(roleId, permissions);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
        }


    }
}
