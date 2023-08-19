#region Using

using Domain.Models.Slider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Domain.ViewModels.SiteSide.Home
{
    #region Main

    public class IndexPageViewModel
    {
        #region properties

        public List<UsersCommentsAboutWebSites> UsersComments { get; set; }

        public List<LastestsArefProducts> LastestsArefProducts { get; set; }

        public List<LatestCategoriesWithImage> LatestCategoriesWithImages { get; set; }

        public List<Slider> Sliders { get; set; }

        #endregion
    }

    #endregion

    #region Lastest Aref's Products

    public class LastestsArefProducts
    {
        #region properties

        public int? ProdudctCategoryId { get; set; }

        public string? ProdudctCategoryTitle { get; set; }

        public List<LastestProducts> LastestProducts { get; set; }

        #endregion
    }

    public class LastestProducts
    {
        #region properties

        public int ProductId { get; set; }

        public string Title { get; set; }

        public bool? IsInOffer { get; set; }

        public string ProductImageName { get; set; }

        public string SecondeProductImageName { get; set; }

        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        #endregion
    }

    #endregion

    #region Latest Categories With Image

    public class LatestCategoriesWithImage
    {
        #region properties

        public int CategoryId { get; set; }

        public string CategoryTitle { get; set; }

        public string ProductImage { get; set; }

        public string Div { get; set; }

        public string P{ get; set; }

        #endregion
    }

    #endregion

    #region Users Comments About WebSites

    public class UsersCommentsAboutWebSites
    {
        #region properties

        public int CommentId{ get; set; }

        public string UserCommentText { get; set; }

        public string Username { get; set; }

        #endregion
    }

    #endregion
}
