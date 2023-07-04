#region Using


using Domain.Models.Common;

#endregion

namespace Domain.Models.UserCommentAboutSite
{
    public class UsersCommentsAboutSite : BaseEntity
    {
        #region properties

        public string CommentText { get; set; }

        public string Username { get; set; }

        #endregion
    }
}


