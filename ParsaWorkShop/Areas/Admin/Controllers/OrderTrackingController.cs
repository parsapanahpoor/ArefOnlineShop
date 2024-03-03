#region Usings

using Application.Interfaces;
using Application.Security;
using Application.ViewModels;
using Domain.Models.Order;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace ParsaWorkShop.Areas.Admin.Controllers
{
    [PermissionChecker(11)]
    public class OrderTrackingController : AdminBaseController
    {
        #region Ctor

        private IOrderService _order;
        private IUserService _userservice;
        private IFinancialTransactionService _financial;
        private IReturendProductsService _returnProducts;
        public OrderTrackingController(IOrderService order, IUserService userService, IFinancialTransactionService financial, IReturendProductsService returnProducts)
        {
            _order = order;
            _userservice = userService;
            _financial = financial;
            _returnProducts = returnProducts;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            return View(await _order.FillListOfOrdersInOrderTrackingIndexPageAdminPanelViewModel());
        }

        #endregion

        #region In Progress Orders 

        #region List Of InProgress Orders

        [HttpGet]
        public async Task<IActionResult> ListOfInProgressOrders()
        {
            return View(await _order.GetListOfInProgressOrders());
        }

        #endregion

        #region Check For Send Order To The Customer

        [HttpGet]
        public async Task<IActionResult> CheckForSendOrderToTheCustomer(int orderId)
        {
            #region Method

            var res = await _order.CheckForSendOrderToTheCustomer(orderId);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(Index));
            }

            #endregion

            TempData[ErrorMessage] = "عملیات باشکست مواجه شده است.";
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #endregion

        #region Check User Information 

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

        #endregion

        #region Check Order Detail

        public async Task<IActionResult> CheckOrderDetails(int? id)
        {
            if (id == null)
            {
                return View();
            }

            return View(await _order.FillListOfOrderDetailsAdminSideViewModel(id.Value));
        }

        #endregion

        #region Accounting List

        public IActionResult AccountingList()
        {
            List<FinancialTransaction> financials = _financial.GetAllFinancialTransaction();

            ViewBag.ExportMoney = _financial.ExportMoney();
            ViewBag.ReciveMoney = _financial.ReciveMoney();

            return View(financials);
        }

        #endregion

        #region List Of Returm Products

        public IActionResult ListOfReturnedProducts()
        {
            List<ReturnedProducts> products = _returnProducts.GetAllReturnedProducts();

            return View(products);
        }

        #endregion

        #region Check Out Request For Returend

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

        #endregion

        #region Decline Return Request

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

        #endregion

        #region Accept Return Request

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

        #endregion
    }
}
