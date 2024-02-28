using System.IO;

namespace Application.StaticTools
{
    public static class PathTools
    {
        #region Site

        public static string SiteFarsiName = "فروشگاه Aref";
        public static string SiteAddress = "https://localhost:5001";
        public static string merchant = "804912ce-546d-4021-9f26-f36bced7f525";

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

        #region Product

        public static readonly string ProductPath = "/Images/Product/image/";
        public static readonly string ProductPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product/image/");

        public static readonly string ProductPathThumb = "/Images/Product/thumb/";
        public static readonly string ProductPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product/thumb/");

        #endregion

        #region Product Gallery

        public static readonly string ProductGalleryPath = "/Images/Product/ProducGallery/";
        public static readonly string ProductGalleryPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product/ProducGallery/");

        #endregion
    }

}

