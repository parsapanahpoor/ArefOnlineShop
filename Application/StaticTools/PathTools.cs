using System.IO;

namespace Application.StaticTools
{
    public static class PathTools
    {
        #region Site

        public static string SiteFarsiName = "فروشگاه Aref";
        public static string SiteAddress = "https://localhost:44334";
        public static string merchant = "8f5510c9-10bd-4eb6-a3e2-2795a2f36edf";

        public static readonly string SiteLogo = "/content/images/site/logo/main/";
        public static readonly string SiteLogoServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/site/logo/main/");

        public static readonly string DefaultSiteLogo = "/content/images/site/logo/default/logo.png";
        public static readonly string SiteLogoThumb = "/content/images/site/logo/thumb/";
        public static readonly string SiteLogoThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/site/logo/thumb/");

        public static readonly string EmailBanner = "/content/images/site/emailBanner/main/";
        public static readonly string EmailBannerServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/site/emailBanner/main/");

        public static readonly string EmailBannerThumb = "/content/images/site/emailBanner/thumb/";
        public static readonly string EmailBannerThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/site/emailBanner/thumb/");

        #endregion

        #region UserAvatar

        public static readonly string UserAvatarPath = "/content/images/user/main/";
        public static readonly string UserAvatarPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/user/main/");

        public static readonly string UserAvatarPathThumb = "/content/images/user/thumb/";
        public static readonly string UserAvatarPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/user/thumb/");

        public static readonly string DefaultUserAvatar = "/content/images/user/DefaultAvatar.png";

        #endregion

        #region Ckeditor

        public static readonly string UploadCkEditorImagePath = "/content/upload/ckeditor/images/";
        public static readonly string UploadCkEditorImagePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/upload/ckeditor/images/");

        #endregion

        #region Slider

        public static readonly string SliderPath = "/Images/Slider/main/";
        public static readonly string SliderPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slider/main/");

        public static readonly string SliderPathThumb = "/Images/Slider/thumb/";
        public static readonly string SliderPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slider/thumb/");

        #endregion

        #region Product Category

        public static readonly string ProductCategoryPath = "/Images/ProductCategory/main/";
        public static readonly string ProductCategoryPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductCategory/main/");

        public static readonly string ProductCategoryPathThumb = "/Images/ProductCategory/thumb/";
        public static readonly string ProductCategoryPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductCategory/thumb/");

        #endregion

        #region Color

        public static readonly string ColorPath = "/Images/Color/main/";
        public static readonly string ColorPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Color/main/");

        public static readonly string ColorPathThumb = "/Images/Color/thumb/";
        public static readonly string ColorPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Color/thumb/");

        #endregion
    }

}

