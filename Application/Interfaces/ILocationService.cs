using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILocationService
    {
        List<Locations> GetAllUserLocations(int userid);
        int AddLocation(int userid, string Address, int Postalcode, string Username, string Mobile, string Email, string CityName, string StateName);
        void AddLocationForUser(int userid, int potalcode, string Address, string Username, string Mobile, string Email, string CityName, string StateName);
        Locations GetLocationByLocationID(int LocationID);
        void UpdateLocation(Locations locations);
        void DeleteLocation(Locations locations);

    }
}
