using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class Common
    {
        public static string LimitTo(this string data, int length)
        {
            return (data == string.Empty || data.Length < length) ? data : data.Substring(0, length);
        }

        public static string GetEnglishNumbers(this string s)
        {
            return s.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
        }

        public static string GetPersianNumbers(this string s)
        {
            return s.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
        }

        public static string SplitUserName(this string name)
        {
            return name.Split()[0];
        }

        public static string GetEnumName(this System.Enum myEnum)
        {
            var enumDisplayName = myEnum.GetType()
                .GetMember(myEnum.ToString())
                .FirstOrDefault();

            if (enumDisplayName != null)
            {
                return enumDisplayName.GetCustomAttribute<DisplayAttribute>().GetName();
            }

            return "";
        }

        public static string FixPrice(this int price)
        {
            if (price > 0)
            {
                return price.ToString("0,0");
            }

            return "0";
        }

        public static string FixPriceOrFree(this int price)
        {
            if (price > 0)
            {
                return $"{price.ToString("0,0")} تومان";
            }

            return "رایگان";
        }
    }
}
