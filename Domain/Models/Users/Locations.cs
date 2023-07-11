using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class Locations
    {
        [Key]
        public int LocationID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        public string LocationAddress { get; set; }

        public int PostalCode { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Mobile { get; set; }

        public string Email { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public string StateName { get; set; }

        #region Navigations
        public User User { get; set; }
        public List<Order.Orders> Orders { get; set; }
        #endregion
    }
}
