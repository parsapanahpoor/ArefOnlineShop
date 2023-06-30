using Application.Interfaces;
using Domain.Models.Order;
using Domain.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        #region Ctor

        private IUserService _user;
        private IProductService _product;
        private IOrderService _order;
        private ILocationService _location;
        private IFinancialTransactionService _financial;

        public OrderController(IUserService user, IProductService product, IOrderService order, ILocationService location,
                                    IFinancialTransactionService financial)
        {
            _user = user;
            _product = product;
            _order = order;
            _location = location;
            _financial = financial;
        }

        #endregion
        public IActionResult AddToShopCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!_product.IsExistPRoduct((int)id))
            {
                return NotFound();
            }
            int userid = _user.GetUserIdByUserName(User.Identity.Name);
            Product product = _product.GetProductByID((int)id);

            if (_order.IsExistOrderFromUserFromToday(userid))
            {
                Orders order = _order.GetOrderForShopCart(userid);

                if (_order.IsExistOrderDetailFromUserFromToday(order.OrderId, (int)id))
                {
                    _order.AddOneMoreProductToTheShopCart(order.OrderId, (int)id);
                }
                else
                {
                    _order.AddProductToOrderDetail(order.OrderId, product.ProductID, product.Price);
                }
            }
            else
            {
                int orderid = _order.AddOrderToTheShopCart(userid);
                _order.AddProductToOrderDetail(orderid, product.ProductID, product.Price);
            }

            return RedirectToAction(nameof(ShopCart));
        }

        public IActionResult ShopCart()
        {
            int userid = _user.GetUserIdByUserName(User.Identity.Name);
            Orders order = _order.GetOrderForShopCart(userid);
            if (order != null)
            {
                ViewBag.orderDetails = _order.GetAllOrderDetailsByOrderID(order.OrderId);

                if (_order.CheckForProductCount(order.OrderId))
                {
                    ViewBag.error = true;
                }
            }

            return View(order);
        }

        public IActionResult RemoveProductFromShopCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _order.RemoveProductFromShopCart((int)id);

            return RedirectToAction(nameof(ShopCart));
        }

        public IActionResult PlusProductFromOrderDetail(int? id)
        {
            if (id == null)
            {
                return View();
            }
            _order.PlusProductToTheOrderDetails((int)id);

            return RedirectToAction(nameof(ShopCart));
        }

        public IActionResult MinusProductFromOrderDetail(int? id)
        {
            if (id == null)
            {
                return View();
            }
            _order.MinusProductToTheOrderDetails((int)id);

            return RedirectToAction(nameof(ShopCart));
        }

        public IActionResult GetTheUserLocations(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int userid = _user.GetUserIdByUserName(User.Identity.Name);

            ViewBag.UserLocations = _location.GetAllUserLocations(userid);
            ViewBag.orderid = id;

            return View();
        }

        [HttpPost]
        public IActionResult AddTheUserLocations(int orderid, string LocationAddress, string PostalCode)
        {
            if (ModelState.IsValid)
            {
                int userid = _user.GetUserIdByUserName(User.Identity.Name);
                int postalCode = int.Parse(PostalCode);

                _location.AddLocation(userid, LocationAddress, postalCode);
            }

            return Redirect("/order/GetTheUserLocations?id=" + orderid);
        }

        public IActionResult AcceptFactor(int? oredrid, int Locationid)
        {
            if (oredrid == null)
            {
                return NotFound();
            }
            int userid = _user.GetUserIdByUserName(User.Identity.Name);
            Orders order = _order.GetOrderByOrderID((int)oredrid);

            Orders NewOrder = _order.UpdateOrderByLocationid(order, Locationid);

            ViewBag.orderDetails = _order.GetAllOrderDetailsByOrderID(NewOrder.OrderId);
            ViewBag.UserLocations = _order.GetUserLocationByOrderId(NewOrder.OrderId);
            ViewBag.OrderID = NewOrder.OrderId;

            return View();
        }
        public IActionResult Payment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Orders order = _order.GetOrderByOrderID((int)id);
            List<OrderDetails> orderDetails = _order.GetAllOrderDetailsByOrderID(order.OrderId);
            int Amount = 0;

            foreach (var item in orderDetails)
            {
                Amount = Amount + (int)(item.Price * item.Count);
            }

            #region Online Payment

            var payment = new ZarinpalSandbox.Payment((int)Amount);

            var res = payment.PaymentRequest("پرداخت  ", "http://zanbilkola.ir/OnlinePayment/" + order.OrderId, "Parsapanahpoor@yahoo.com", "09117878804");

            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }

            #endregion

            return View();
        }

        [Route("OnlinePayment/{id}")]
        public IActionResult onlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                Orders order = _order.GetOrderByOrderID(id);
                List<OrderDetails> orderDetails = _order.GetAllOrderDetailsByOrderID(id);

                int Amount = 0;

                foreach (var item in orderDetails)
                {
                    Amount = Amount + (int)(item.Price * item.Count);
                }

                var payment = new ZarinpalSandbox.Payment(Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    _order.IsfinallyForOredr(order);
                    _financial.AddFinancialTransactionAfterPaymentOrder(order.OrderId, Amount, "", "");

                    foreach (var item in orderDetails)
                    {
                        _product.MinusProductCountAfterSale(item.ProductID, item.Count);
                    }
                }
            }

            return View();
        }
    }
}
