using AppContext.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wpf.Tools
{
    public static class Initializator
    {
        public static User CreateNewUserWithAdminPrivilegesAtInitialization(User user)
        {
            List<Permission> permissionList = new List<Permission>()
            {
                new Permission() { Name = "New user creation" }
            };
            List<Role> roleList = new List<Role>()
            {
                new Role() { Name = "Admin", Permissions = permissionList }
            };

            user.Roles = roleList;
            
            return user;
        }

        internal static void UpdateNewUserWithRole(User newUser, IRepository repository)
        {
            repository.UpdateUser(newUser);
        }
    }
}
