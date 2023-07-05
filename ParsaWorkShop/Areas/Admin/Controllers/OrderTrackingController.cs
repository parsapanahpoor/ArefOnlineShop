using Application.Interfaces;
using Application.Security;
using Application.ViewModels;
using Domain.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    //[PermissionChecker(1)]

    public class OrderTrackingController : Controller
    {
        private IOrderService _order;
        private IUserService _userservice;
        private IFinancialTransactionService _financial;
        private IReturendProductsService _returnProducts;
        public OrderTrackingController(IOrderService order, IUserService userService, IFinancialTransactionService financial , IReturendProductsService returnProducts)
        {
            _order = order;
            _userservice = userService;
            _financial = financial;
            _returnProducts = returnProducts;
        }

        public IActionResult Index()
        {
            List<Orders> Orders = _order.GetAllOrdersForShowInAdminPanel();

            return View(Orders);
        }

        public IActionResult CheckUserInformation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Orders order = _order.GetOrderByOrderID((int)id);
            EditUserViewModel user = _userservice.GetUserForShowInEditMode((int)order.Userid);
            ViewBag.Location = _order.GetUserLocationByOrderID((int)id);

            return View(user);
        }

        public IActionResult CheckOrderDetails(int? id)
        {
            if (id == null)
            {
                return View();
            }

            List<OrderDetails> orderDetails = _order.GetAllOrderDetailsByOrderID((int)id);

            return View(orderDetails);
        }

        public IActionResult AccountingList()
        {
            List<FinancialTransaction> financials = _financial.GetAllFinancialTransaction();

            ViewBag.ExportMoney = _financial.ExportMoney();
            ViewBag.ReciveMoney = _financial.ReciveMoney();

            return View(financials);
        }

        public IActionResult ListOfReturnedProducts()
        {
            List<ReturnedProducts> products = _returnProducts.GetAllReturnedProducts();

            return View(products);
        }

        public IActionResult CheckOutRequestForReturend(int? id)
        {
            if (id == null)
            {
                return View();
            }

            ReturnedProducts products = _returnProducts.GetReturendProductByID((int)id);

            if (products == null)
            {
                return View();
            }
            OrderDetails orderDetail = _order.GetOrderDetailByID(products.OrderDetailID);
            ViewBag.Location = _order.GetUserLocationByOrderID(orderDetail.OrderID);

            return View(products);
        }

        public IActionResult DeclineReturnRequest(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ReturnedProducts returned = _returnProducts.GetReturendProductByID((int)id);
            if (returned == null)
            {
                return NotFound();
            }
            _returnProducts.DeclineReturnRequest(returned);

            return RedirectToAction(nameof(ListOfReturnedProducts));
        }

        public IActionResult AcceptReturnRequest(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }
            ReturnedProducts returned = _returnProducts.GetReturendProductByID((int)id);
            if (returned == null)
            {
                return NotFound();
            }
            _returnProducts.AcceptReturnRequest(returned);

            OrderDetails orderDetail = _order.GetOrderDetailByID(returned.OrderDetailID);
            decimal price = _order.GetPriceOfOrderDetailByOrderDetailID(returned.OrderDetailID);
            _financial.AddFinancialTransactionForReturendProduct(orderDetail.OrderID, (int)price, "", "");
            _order.ReturnedProduct(orderDetail);

            return RedirectToAction(nameof(ListOfReturnedProducts));
        }
    }
}
