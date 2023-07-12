using Domain.Models.Product;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.UserPanel.Dashboard
{
    public class UserPanelDashboardViewModel 
    {
        #region properties

        public List<Product> LastOrder { get; set; }

        public List<Product> LastFavoriteProduct { get; set; }

        public User User { get; set; }

        #endregion
    }
}
