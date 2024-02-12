#region Usings

using AngleSharp.Io;
using Application.Convertors;
using Application.Extensions;
using Application.Interfaces;
using Application.Services;
using Application.StaticTools;
using Domain.DTOs.ZarinPal;
using Domain.Models.Order;
using Domain.Models.Product;
using Domain.Models.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using ParsaWorkShop.Web.Controllers;
using Domain.ViewModels.SiteSide.Order;
using Microsoft.CodeAnalysis;
using Domain.Models.Users;
using OfficeOpenXml.Style;

#endregion

namespace ParsaWorkShop.Controllers
{
    [Authorize]
    public class OrderController : SiteBaseController
    {
        #region Ctor

        private IUserService _user;
        private IProductService _product;
        private IOrderService _order;
        private ILocationService _location;
        private IFinancialTransactionService _financial;
        private readonly IWalletService _walletService;
        private readonly IDiscountCodeService _discountCodeService;

        public OrderController(IUserService user, IProductService product, IOrderService order, ILocationService location,
                                    IFinancialTransactionService financial, IWalletService walletService, IDiscountCodeService discountCodeService)
        {
            _user = user;
            _product = product;
            _order = order;
            _location = location;
            _financial = financial;
            _walletService = walletService;
            _discountCodeService = discountCodeService;
        }

        #endregion

        #region Add To Shop Cart

        [HttpPost, ValidateAntiForgeryToken , AllowAnonymous]
        public async Task<IActionResult> AddToShopCart(IncomingProductInBasketSiteSideViewModel model)
        {
            #region Model State Validation

            if (!User.Identity.IsAuthenticated) 
            {
                return RedirectToAction("Login" , "Account");
            }

            if (!model.selectColor.HasValue || !model.selectSize.HasValue)
            {
                TempData[ErrorMessage] = "You Must Choose Size And Color";
                return RedirectToAction("SinglePageProducts", "Products", new { id = model.id, ProductTitle = await _product.GetProductTitleWithProductId(model.id.Value) });
            }

            if (model.id == null)
            {
                return NotFound();
            }

            if (!_product.IsExistPRoduct((int)model.id))
            {
                return NotFound();
            }

            if (!await _product.CheckThatIsExistProductWithThisColor(model.id.Value, model.selectColor.Value))
            {
                TempData[ErrorMessage] = "There is a problem with datas";
                return RedirectToAction("SinglePageProducts", "Products", new { id = model.id, ProductTitle = await _product.GetProductTitleWithProductId(model.id.Value) });
            }

            if (!await _product.CheckThatIsExistProductWithThisSize(model.id.Value, model.selectSize.Value))
            {
                TempData[ErrorMessage] = "There is a problem with datas";
                return RedirectToAction("SinglePageProducts", "Products", new { id = model.id, ProductTitle = await _product.GetProductTitleWithProductId(model.id.Value) });
            }

            if (model.Count <= 0)
            {
                TempData[ErrorMessage] = "Count Of Request Must More Than 0";
                return RedirectToAction("SinglePageProducts", "Products", new { id = model.id, ProductTitle = await _product.GetProductTitleWithProductId(model.id.Value) });
            }

            #endregion

            #region Initial Order

            //Get UserId 
            int userid = User.GetUserId();

            //Get Product By Product Id
            Product product = _product.GetProductByID((int)model.id);

            //Is Exit Any Order For Current User Today
            if (_order.IsExistOrderFromUserFromToday(userid))
            {
                Orders order = _order.GetOrderForShopCart(userid);

                #region Check User Last Part Of Shop Cart

                if (await _order.IsOrderInLastStepOfShoping(order.OrderId , User.GetUserId()))
                {
                    return RedirectToAction(nameof(AcceptFactor) , new { oredrid = order.OrderId , Locationid = order.LocationID });
                }

                #endregion

                if (_order.IsExistOrderDetailFromUserFromToday(order.OrderId, (int)model.id, model.selectColor.Value, model.selectSize.Value))
                {
                    _order.AddOneMoreProductToTheShopCart(order.OrderId, (int)model.id, model.selectColor.Value, model.selectSize.Value, model.Count);
                }
                else
                {
                    _order.AddProductToOrderDetail(order.OrderId, product.ProductID, product.Price, model.selectColor.Value, model.selectSize.Value, model.Count);
                }
            }
            else
            {
                //Order To The Data Base
                int orderid = _order.AddOrderToTheShopCart(userid);

                //Add Order Detail To The Data Base 
                _order.AddProductToOrderDetail(orderid, product.ProductID, product.Price, model.selectColor.Value, model.selectSize.Value, model.Count);
            }

            #endregion

            return RedirectToAction(nameof(ShopCart));
        }

        #endregion

        #region Shop Cart

        public async Task<IActionResult> ShopCart()
        {
            var order = await _order.FillInvoiceSiteSideViewModel(User.GetUserId());

            if (order == null)
            {
                TempData[ErrorMessage] = "There Is not Any Product in your Shop Cart. ";
                return RedirectToAction("Index", "Home");
            }

            #region Check User Last Part Of Shop Cart

            if (await _order.IsOrderInLastStepOfShoping(order.Order.OrderId, User.GetUserId()))
            {
                return RedirectToAction(nameof(AcceptFactor), new { oredrid = order.Order.OrderId, Locationid = order.Order.LocationID });
            }

            #endregion

            if (order == null)
            {
                TempData[ErrorMessage] = "There Is not Any Product in your Shop Cart. ";
                return RedirectToAction("Index", "Home");
            } 

            if (order != null)
            {
                ViewBag.orderDetails = _order.GetAllOrderDetailsByOrderID(order.Order.OrderId);
            }

            return View(order);
        }


        #endregion

        #region Remove Product From Shop Cart

        public async Task<IActionResult> RemoveProductFromShopCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            #region Get Order By Order Detail Id 

            Orders order = _order.GetOrderByOrderDetailId(id.Value);

            #region Check User Last Part Of Shop Cart

            if (await _order.IsOrderInLastStepOfShoping(order.OrderId, User.GetUserId()))
            {
                return RedirectToAction(nameof(AcceptFactor), new { oredrid = order.OrderId, Locationid = order.LocationID });
            }

            #endregion

            #endregion

            _order.RemoveProductFromShopCart((int)id);

            return RedirectToAction(nameof(ShopCart));
        }

        #endregion

        #region Plus Product From Order Detail

        public async Task<IActionResult> PlusProductFromOrderDetail(int? id)
        {
            if (id == null)
            {
                return View();
            }

            #region Get Order By Order Detail Id 

            Orders order = _order.GetOrderByOrderDetailId(id.Value);

            #region Check User Last Part Of Shop Cart

            if (await _order.IsOrderInLastStepOfShoping(order.OrderId, User.GetUserId()))
            {
                return RedirectToAction(nameof(AcceptFactor), new { oredrid = order.OrderId, Locationid = order.LocationID });
            }

            #endregion

            #endregion


            _order.PlusProductToTheOrderDetails((int)id);

            return RedirectToAction(nameof(ShopCart));
        }

        #endregion

        #region Minus Product From Order Detail

        public async Task<IActionResult> MinusProductFromOrderDetail(int? id)
        {
            if (id == null)
            {
                return View();
            
            }

            #region Get Order By Order Detail Id 

            Orders order = _order.GetOrderByOrderDetailId(id.Value);

            #region Check User Last Part Of Shop Cart

            if (await _order.IsOrderInLastStepOfShoping(order.OrderId, User.GetUserId()))
            {
                return RedirectToAction(nameof(AcceptFactor), new { oredrid = order.OrderId, Locationid = order.LocationID });
            }

            #endregion

            #endregion


            _order.MinusProductToTheOrderDetails((int)id);

            return RedirectToAction(nameof(ShopCart));
        }

        #endregion

        #region Get The User Locations

        public async Task<IActionResult> GetTheUserLocations(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            #region Get Order By Order Id 

            Orders order = _order.GetOrderByOrderID(id.Value);

            #region Check User Last Part Of Shop Cart

            if (await _order.IsOrderInLastStepOfShoping(order.OrderId, User.GetUserId()))
            {
                return RedirectToAction(nameof(AcceptFactor), new { oredrid = order.OrderId, Locationid = order.LocationID });
            }

            #endregion

            #endregion

            int userid = _user.GetUserIdByUserName(User.Identity.Name);

            ViewBag.UserLocations = _location.GetAllUserLocations(userid);
            ViewBag.orderid = id;

            return View();
        }

        #endregion

        #region Add The User Locations

        [HttpPost]
        public async Task<IActionResult> AddTheUserLocations(int orderid, string LocationAddress, string PostalCode, string Username, string Mobile, string Email, string CityName, string StateName)
        {
            #region Get Order By Order Id 

            Orders order = _order.GetOrderByOrderID(orderid);

            #region Check User Last Part Of Shop Cart

            if (await _order.IsOrderInLastStepOfShoping(order.OrderId, User.GetUserId()))
            {
                return RedirectToAction(nameof(AcceptFactor), new { oredrid = order.OrderId, Locationid = order.LocationID });
            }

            #endregion

            #endregion

            if (ModelState.IsValid)
            {
                int userid = _user.GetUserIdByUserName(User.Identity.Name);
                int postalCode = int.Parse(PostalCode);

                var locationId = _location.AddLocation(userid, LocationAddress, postalCode, Username, Mobile, Email, CityName, StateName);

                return RedirectToAction(nameof(AcceptFactor), new { oredrid = orderid, Locationid = locationId });
            }

            return RedirectToAction(nameof(GetTheUserLocations), new { id = orderid });
        }

        #endregion

        #region Accept Factor

        public async Task<IActionResult> AcceptFactor(int? oredrid, int Locationid, int? discountPrice)
        {
            if (oredrid == null) return NotFound();

            Orders order = _order.GetOrderByOrderID((int)oredrid);

            Orders NewOrder = _order.UpdateOrderByLocationid(order, Locationid);

            ViewBag.orderDetails = _order.GetAllOrderDetailsByOrderID(NewOrder.OrderId);
            ViewBag.UserLocations = _order.GetUserLocationByOrderId(NewOrder.OrderId);
            ViewBag.OrderID = NewOrder.OrderId;
            ViewBag.Locationid = Locationid;
            ViewBag.discountPrice = discountPrice;

            return View(await _order.FillInvoiceSiteSideViewModel(User.GetUserId()));

        }

        #endregion

        #region Delete Shop Cart 

        public async Task<IActionResult> DeleteShopCart(int orderId)
        {
            #region Delete User Shop Cart 

            var res = await _order.DeleteUserOrder(orderId , User.GetUserId());
            if (res)
            {
                TempData[SuccessMessage] = "Operation Has Been Successfully";
                return RedirectToAction("Index" , "Home");
            }

            #endregion

            return NotFound();
        }

        #endregion

        #region Add Discount For Order

        [HttpPost]
        public async Task<IActionResult> AddDiscountForOrder(int orderId, int Locationid, string DiscountCode)
        {
            #region Add Discount To The Order

            var res = await _discountCodeService.AddDiscountToTheOrder(orderId, User.GetUserId(), DiscountCode);
            if (res != null)
            {
                TempData[SuccessMessage] = "operation has been successfully";
                return RedirectToAction(nameof(AcceptFactor), new { oredrid = orderId, Locationid = Locationid, discountPrice = res });
            }

            #endregion

            TempData[ErrorMessage] = "operation has been faild";
            return RedirectToAction(nameof(AcceptFactor), new { oredrid = orderId, Locationid = Locationid });
        }

        #endregion

        #region Payment

        public async Task<IActionResult> Payment(int? id)
        {
            #region Initial Order Amount

            if (id == null) return NotFound();

            Orders order = _order.GetOrderByOrderID((int)id);
            List<OrderDetails> orderDetails = _order.GetAllOrderDetailsByOrderID(order.OrderId);

            int Amount = 250000;
            //int Amount = 0;

            foreach (var item in orderDetails)
            {
                Amount = Amount + (int)(item.Price * item.Count);
            }

            #region Add Discount To Pice

            if (order.DiscountUserSelected.HasValue)
            {
                Amount = await _discountCodeService.AddDiscountToTheOrderPrice(order.DiscountUserSelected.Value, Amount);

                //Update Order
                order.Price = Amount;
                await _discountCodeService.UpdateOrder(order);

                return RedirectToAction("PaymentMethod", "Payment", new
                {
                    gatewayType = GatewayType.Zarinpal,
                    amount = Amount,
                    description = "شارژ حساب کاربری برای پرداخت هزینه ی حرید",
                    returURL = $"{PathTools.SiteAddress}/OnlinePayment/" + order.OrderId,
                    orderId = order.OrderId,

                });
            }
            else
            {
                //Update Order
                order.Price = Amount;
                await _discountCodeService.UpdateOrder(order);

                return RedirectToAction("PaymentMethod", "Payment", new
                {
                    gatewayType = GatewayType.Zarinpal,
                    amount = Amount,
                    description = "شارژ حساب کاربری برای پرداخت هزینه ی حرید",
                    returURL = $"{PathTools.SiteAddress}/OnlinePayment/" + order.OrderId,
                    orderId = order.OrderId,

                });
            }

            #endregion

            #endregion
        }


        #endregion

        #region online Payment

        [Route("OnlinePayment/{id}")]
        public async Task<IActionResult> onlinePayment(int id)
        {
            #region Get User By User Id

            var user = await _user.GetUserByIdAsync(User.GetUserId());
            if (user == null) NotFound();

            #endregion

            #region Initial Order Amount

            Orders order = _order.GetOrderByOrderID(id);
            List<OrderDetails> orderDetails = _order.GetAllOrderDetailsByOrderID(order.OrderId);

            int Amount = order.Price.Value;

            #endregion

            try
            {
                #region Fill Parametrs

                VerifyParameters parameters = new VerifyParameters();

                if (HttpContext.Request.Query["Authority"] != "")
                {
                    parameters.authority = HttpContext.Request.Query["Authority"];
                }

                parameters.amount = Amount.ToString();
                parameters.merchant_id = PathTools.merchant;

                #endregion

                using (HttpClient client = new HttpClient())
                {
                    #region Verify Payment

                    var json = JsonConvert.SerializeObject(parameters);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(URLs.verifyUrl, content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject jodata = JObject.Parse(responseBody);

                    string data = jodata["data"].ToString();

                    JObject jo = JObject.Parse(responseBody);

                    string errors = jo["errors"].ToString();

                    #endregion

                    if (data != "[]")
                    {
                        //Authority Code
                        string refid = jodata["data"]["ref_id"].ToString();

                        //Get Wallet Transaction For Validation 
                        var wallet = await _walletService.FindWalletTransactionForRedirectToTheBankPortal(user.UserId, GatewayType.Zarinpal, order.OrderId, parameters.authority, Amount);

                        if (wallet != null)
                        {
                            //Charge User Wallet
                            await _walletService.UpdateWalletAndCalculateUserBalanceAfterBankingPayment(wallet);

                            //Pay Order Amount 
                            await _walletService.PayOrderAmount(user.UserId, Amount, order.OrderId);

                            #region Finalize Order

                            _order.IsfinallyForOredr(order);

                            foreach (var item in orderDetails)
                            {
                                _product.MinusProductCountAfterSale(item.ProductID, item.Count);
                            }

                            #endregion

                            return RedirectToAction("PaymentResult", "Payment", new { IsSuccess = true, refId = refid });
                        }
                    }
                    else if (errors != "[]")
                    {
                        string errorscode = jo["errors"]["code"].ToString();

                        return RedirectToAction("PaymentResult", "Payment", new { IsSuccess = false, refId = 123 });
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return NotFound();
        }

        #endregion
    }
}
