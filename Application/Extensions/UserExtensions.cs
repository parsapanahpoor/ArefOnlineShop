using Application.StaticTools;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace Application.Extensions
{
    public static class UserExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var data = claimsPrincipal.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier);

            return Int32.Parse(data.Value);
        }

        public static int GetUserId(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;

            return user.GetUserId();
        }

        public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
        {
            var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Name);

            return data.ToString();
        }

        public static string GetUsername(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;

            return user.GetUsername();
        }

        public static string GetUserAvatar(this User user)
        {
            if (!string.IsNullOrEmpty(user.UserAvatar))
            {
                return Path.Combine(PathTools.UserAvatarPathThumb, user.UserAvatar);
            }

            return PathTools.DefaultUserAvatar;
        }

        public static string GetUserAvatar(this string userAvatar)
        {
            if (!string.IsNullOrEmpty(userAvatar))
            {
                return Path.Combine(PathTools.UserAvatarPathThumb, userAvatar);
            }

            return PathTools.DefaultUserAvatar;
        }
    }
}
