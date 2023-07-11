using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LocationService : ILocationService
    {
        private ILocationRepository _location;

        public LocationService(ILocationRepository location)
        {
            _location = location;
        }

        public void AddLocation(int userid, string Address, int Postalcode)
        {
            Locations location = new Locations()
            {
                UserID = userid,
                LocationAddress = Address,
                PostalCode = Postalcode
            };

            _location.AddLocation(location);
        }

        public void AddLocationForUser(int userid, int potalcode, string Address, string Username, string Mobile, string Email, string CityName, string StateName)
        {
            Locations locations = new Locations()
            {
                UserID = userid,
                PostalCode = potalcode,
                LocationAddress = Address,
                CityName = CityName,
                Email = Email,
                Mobile = Mobile,
                StateName = StateName,
                Username = Username
            };

            _location.AddLocation(locations);
        }

        public void DeleteLocation(Locations locations)
        {
            _location.DeleteLocation(locations);
        }

        public List<Locations> GetAllUserLocations(int userid)
        {
            return _location.GetAllUserLocations(userid);
        }

        public Locations GetLocationByLocationID(int LocationID)
        {
            return _location.GetLocationByLocationID(LocationID);
        }

        public void UpdateLocation(Locations locations)
        {
            _location.UpdateLocation(locations);
        }
    }
}
