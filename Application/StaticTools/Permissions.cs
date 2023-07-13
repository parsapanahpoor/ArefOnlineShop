#region Using

using Domain.Models.Permissions;
using System;
using System.Collections.Generic;

#endregion

namespace Application.StaticTools
{
    public static class PermissionsList
    {
        public static List<Permission> Permissions
        {
            get
            {
                List<Permission> list = new List<Permission>();

                var date = new DateTime(2023, 07, 19);

                #region Add Permissions

                #region Dashboard

                list.Add(new Permission
                {
                    PermissionId = 1,
                    ParentID = null,
                    PermissionTitle = "داشبورد"
                });

                #endregion

                #region Blog And Blog Categories

                list.Add(new Permission
                {
                    PermissionId = 2,
                    ParentID = null,
                    PermissionTitle = "بلاگ و دسته بندی آن ها"
                });

                #endregion

                #region Discount Code

                list.Add(new Permission
                {
                    PermissionId = 3,
                    ParentID = null,
                    PermissionTitle = "کد تخفیف "
                });

                #endregion

                #region سفارش ها

                list.Add(new Permission
                {
                    PermissionId = 4,
                    ParentID = null,
                    PermissionTitle = "پیگیری سفارشات"
                });

                #endregion

                #region Product And Product Categories

                list.Add(new Permission
                {
                    PermissionId = 5,
                    ParentID = null,
                    PermissionTitle = "محصولات و دسته بندی آن ها"
                });

                #endregion

                #region Site Setting

                list.Add(new Permission
                {
                    PermissionId = 6,
                    ParentID = null,
                    PermissionTitle = "تنظیمات سایت"
                });

                #endregion

                #region Slider

                list.Add(new Permission
                {
                    PermissionId = 7,
                    ParentID = null,
                    PermissionTitle = "اسلایدر"
                });

                #endregion

                #region Users

                list.Add(new Permission
                {
                    PermissionId = 8,
                    ParentID = null,
                    PermissionTitle = "کاربران"
                });

                #endregion

                #region Videos

                list.Add(new Permission
                {
                    PermissionId = 9,
                    ParentID = null,
                    PermissionTitle = "ویدیو ها"
                });

                #endregion

                #endregion

                // Last Id Use is : 9

                return list;
            }
        }
    }
}
