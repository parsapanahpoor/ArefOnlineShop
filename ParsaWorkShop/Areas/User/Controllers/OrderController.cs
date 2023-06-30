using Application.Interfaces;
using Domain.Models.Order;
using Domain.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class OrderController : Controller
    {
        #region Ctor
        private IUserService _userService;
        private ILocationService _location;
        private IOrderService _order;
        private IReturendProductsService _returnProduct;
        private IProductService _product;

        public OrderController(IUserService userService, ILocationService location, IOrderService order, IReturendProductsService returnProduct,
                                IProductService product)
        {
            _userService = userService;
            _location = location;
            _order = order;
            _returnProduct = returnProduct;
            _product = product;
        }
        #endregion

        public IActionResult Index()
        {
            int userid = _userService.GetUserIdByUserName(User.Identity.Name);

            List<Orders> orders = _order.GetOrdersByUsersId(userid);
            return View(orders);
        }
        public IActionResult OrderDetails(int? id)
        {
            if (id == null)
            {
                return View();
            }
            List<OrderDetails> orderDetails = _order.GetAllOrderDetailsByOrderID((int)id);

            return View(orderDetails);
        }

        public IActionResult ListOfReturnedProducts()
        {
            int userid = _userService.GetUserIdByUserName(User.Identity.Name);

            return View(_returnProduct.GetAllReturendProductsByUserid(userid));
        }

        public IActionResult RequestForReturnProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            OrderDetails orderDetail = _order.GetOrderDetailByID((int)id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            Product product = _product.GetProductByID(orderDetail.ProductID);

            ViewBag.ProductTitle = product.ProductTitle;
            ViewBag.OrderDetailID = orderDetail.OrderDetailID;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestForReturnProduct(ReturnedProducts returned)
        {
            if (ModelState.IsValid)
            {
                _returnProduct.AddRequestForReturnedProduct(returned, _userService.GetUserIdByUserName(User.Identity.Name));

                return RedirectToAction(nameof(ListOfReturnedProducts));
            }

            OrderDetails orderDetail = _order.GetOrderDetailByID(returned.OrderDetailID);
            Product product = _product.GetProductByID(orderDetail.ProductID);

            ViewBag.ProductTitle = product.ProductTitle;
            ViewBag.OrderDetailID = orderDetail.OrderDetailID;

            return View(returned);
        }
    }
}
