using Application.Interfaces;
using Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class LocationController : Controller
    {
        #region Location
        private IUserService _userService;
        private ILocationService _location;
        private IOrderService _order;
        public LocationController(IUserService userService, ILocationService lcoation, IOrderService order)
        {
            _userService = userService;
            _location = lcoation;
            _order = order;
        }
        #endregion


        public IActionResult Locations()
        {
            var userid = _userService.GetUserIdByUserName(User.Identity.Name);
            var Locations = _location.GetAllUserLocations(userid);

            return View(Locations);
        }
        public IActionResult AddNewLocation()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewLocation(int PostalCode, string LocationAddress)
        {
            if (ModelState.IsValid)
            {
                var userid = _userService.GetUserIdByUserName(User.Identity.Name);
                _location.AddLocationForUser(userid, PostalCode, LocationAddress);

                return RedirectToAction(nameof(Locations));
            }

            return View();
        }

        public IActionResult EditLocation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Locations location = _location.GetLocationByLocationID((int)id);

            return View(location);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditLocation(Locations locations)
        {
            if (ModelState.IsValid)
            {
                if (_order.IsExistLocationInOrder(locations.LocationID))
                {
                    return NotFound();
                }
                else
                {
                    _location.UpdateLocation(locations);
                    return RedirectToAction(nameof(Locations));
                }
            }
            return View(locations);
        }

        public IActionResult DeleteLocation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Locations locations = _location.GetLocationByLocationID((int)id);

            if (_order.IsExistLocationInOrder(locations.LocationID))
            {
                return NotFound();
            }
            else
            {
                _location.DeleteLocation(locations);
            }

            return RedirectToAction(nameof(Locations));
        }
    }
}
