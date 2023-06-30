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
        void AddLocation(int userid, string Address, int Postalcode);
        void AddLocationForUser(int userid, int potalcode, string Address);
        Locations GetLocationByLocationID(int LocationID);
        void UpdateLocation(Locations locations);
        void DeleteLocation(Locations locations);

    }
}
