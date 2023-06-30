using Data.Context;
using Domain.Interfaces;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private ParsaWorkShopContext _context;
        public LocationRepository(ParsaWorkShopContext context)
        {
            _context = context;
        }

        public void AddLocation(Locations locations)
        {
            _context.Locations.Add(locations);
            Savechanges();
        }

        public void DeleteLocation(Locations locations)
        {
            _context.Locations.Remove(locations);
            Savechanges();
        }

        public List<Locations> GetAllUserLocations(int userid)
        {
            return _context.Locations.Where(p => p.UserID == userid).ToList();
        }

        public Locations GetLocationByLocationID(int LocationID)
        {
            return _context.Locations.Find(LocationID);
        }

        public void Savechanges()
        {
            _context.SaveChanges();
        }

        public void UpdateLocation(Locations locations)
        {
            _context.Locations.Update(locations);
            Savechanges();
        }
    }
}
