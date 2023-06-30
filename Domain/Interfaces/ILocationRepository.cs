using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILocationRepository
    {
        List<Locations> GetAllUserLocations(int userid);
        void AddLocation(Locations locations);
        Locations GetLocationByLocationID(int LocationID);
        void UpdateLocation(Locations locations);
        void DeleteLocation(Locations locations);
        void Savechanges();

    }
}
