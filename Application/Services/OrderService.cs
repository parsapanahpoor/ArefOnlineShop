using Application.Convertors;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models.Order;
using Domain.Models.Users;
using Domain.ViewModels.Admin.Order;
using Domain.ViewModels.SiteSide.Order;
using Domain.ViewModels.UserPanel.Orders;
using Microsoft.Extensions.Primitives;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        #region Ctor

        private IOrderRepository _order;
        private readonly ISiteSettingRepsitory _siteSettingRepsitory;
        private static readonly HttpClient client = new HttpClient();

        public OrderService(IOrderRepository order,
                            ISiteSettingRepsitory siteSettingRepsitory)
        {
            _order = order;
            _siteSettingRepsitory = siteSettingRepsitory;
        }

        #endregion

        #region Side Side 

        public async Task<FinalInvoiceSiteSideDTO?> ShowFinalInvoice(int orderId,
                                                                    CancellationToken cancellationChange)
        {
            //Get OrderBy Id 
            var order = GetOrderByOrderID(orderId);
            if (order == null) return null;

            return await _order.ShowFinalInvoice(order, cancellationChange);
        }

        public async Task SendSMSForSubmitedOrder(string? orderId)
        {
            var dateTime = DateTime.Now.ToShamsi();

            var AdminMobilePhone = await _siteSettingRepsitory.GetAdminMobilePhone();
            if (!string.IsNullOrEmpty(AdminMobilePhone))
            {
                #region Send Verification Code SMS

                var result = $"https://api.kavenegar.com/v1/58556757466E4D63554A6339306F5775716946572F6B414577596137334A722B4570575842725845786D453D/verify/lookup.json?receptor={AdminMobilePhone}&token={orderId}&token2={dateTime}&template=BuyAlert";
                var results = client.GetStringAsync(result);

                #endregion
            }
        }

        public async Task SendSMSForUserAboutInvoice(int orderId, string refId, string mobile)
        {
            var link = $"https://arefset.com/Order/ShowInvoice/{orderId}";

            #region Send Verification Code SMS

            var result = $"https://api.kavenegar.com/v1/58556757466E4D63554A6339306F5775716946572F6B414577596137334A722B4570575842725845786D453D/verify/lookup.json?receptor={mobile}&token={link}&template=TanksForBuy";
            var results = client.GetStringAsync(result);

            #endregion
        }

        public void AddOneMoreProductToTheShopCart(int orderid, int productid, int colorId, int sizeId, int count)
        {
            OrderDetails orderDetails = _order.AddOneMoreProductToTheShopCart(orderid, productid, colorId, sizeId);
            orderDetails.Count = orderDetails.Count + count;

            _order.UpdateOrderDetail(orderDetails);
        }

        public int AddOrderToTheShopCart(int userid)
        {
            #region Initial Order

            Orders order = new Orders()
            {
                Userid = userid,
                CreateDate = DateTime.Now,
                IsFinally = false,
                LocationID = null
            };
            _order.AddOrderToTheShopCart(order);

            #endregion

            return order.OrderId;
        }

        public void AddProductToOrderDetail(int OrderID, int ProductID, decimal Price, int colorId, int sizeId, int count)
        {
            #region Fill Order Detail

            OrderDetails orderDetails = new OrderDetails()
            {
                OrderID = OrderID,
                ProductID = ProductID,
                Price = Price,
                Count = count,
                ColorId = colorId,
                SizeId = sizeId
            };

            #endregion

            _order.AddOrderDetails(orderDetails);
        }

        public bool CheckForProductCount(int Orderid)
        {
            List<OrderDetails> orderDetails = GetAllOrderDetailsByOrderID(Orderid);

            bool result = false;

            foreach (var item in orderDetails)
            {
                int productCount = item.Product.ProductCount;
                int OrderdetailsCount = item.Count;

                if (OrderdetailsCount > productCount)
                {
                    result = true;
                }
            }

            return result;
        }

        public List<OrderDetails> GetAllOrderDetailsByOrderID(int orderid)
        {
            return _order.GetAllOrderDetailsByOrderID(orderid);
        }

        //Fill List Of User Orders Details User Side View Model
        public async Task<List<ListOfUserOrdersDetailsUserSideViewModel>> FillListOfUserOrdersDetailsUserSideViewModel(int orderId)
        {
            return await _order.FillListOfUserOrdersDetailsUserSideViewModel(orderId);
        }

        public List<Orders> GetAllOrdersForShowInAdminPanel()
        {
            return _order.GetAllOrdersForShowInAdminPanel();
        }

        public async Task<ListOfOrdersInOrderTrackingIndexPageAdminPanelViewModel> FillListOfOrdersInOrderTrackingIndexPageAdminPanelViewModel()
        {
            ListOfOrdersInOrderTrackingIndexPageAdminPanelViewModel model = new ListOfOrdersInOrderTrackingIndexPageAdminPanelViewModel();

            #region Fill Model 

            //List Of Orders
            model.Orders = GetAllOrdersForShowInAdminPanel();

            model.CountOfFinishedOrders = model.Orders.Count(p => p.OrderState == OrderState.SentToTheCustomer);
            model.CountOfCancelationRequests = model.Orders.Count(p => p.OrderState == OrderState.CancelationRequest);
            model.CountOfInProcessOrders = model.Orders.Count(p => p.OrderState == OrderState.InProccess);

            #endregion

            return model;
        }

        //Get List Of In Progress Orders
        public async Task<ListOfInProgressOrdersAdminSideViewModel> GetListOfInProgressOrders()
        {
            ListOfInProgressOrdersAdminSideViewModel model = new ListOfInProgressOrdersAdminSideViewModel();

            model.Orders = await _order.GetListOfInProgressOrders();

            return model;
        }

        public Orders GetOrderByOrderID(int orderid)
        {
            return _order.GetOrderByOrderID(orderid);
        }

        public OrderDetails GetOrderDetailByID(int orderdetailid)
        {
            return _order.GetOrderDetailByID(orderdetailid);
        }

        public Orders GetOrderForShopCart(int userid)
        {
            return _order.GetOrderForShopCart(userid);
        }

        public List<Orders> GetOrdersByUsersId(int userid)
        {
            return _order.GetOrdersByUsersId(userid);
        }

        public decimal GetPriceOfOrderDetailByOrderDetailID(int OrderdetailID)
        {
            OrderDetails orderDetail = GetOrderDetailByID(OrderdetailID);

            return orderDetail.Price * orderDetail.Count;
        }

        public Locations GetUserLocationByOrderId(int orderid)
        {
            return _order.GetUserLocationByOrderId(orderid);
        }

        public Locations GetUserLocationByOrderID(int orderid)
        {
            return _order.GetUserLocationByOrderID(orderid);
        }

        public bool IsExistLocationInOrder(int Locationid)
        {
            if (_order.IsExistLocationInOrder(Locationid))
            {
                return true;
            }
            return false;
        }

        public bool IsExistOrderDetailFromUserFromToday(int orderid, int productid, int colorId, int sizeId)
        {
            return _order.IsExistOrderDetailFromUserFromToday(orderid, productid, colorId, sizeId);
        }

        public bool IsExistOrderFromUserFromToday(int userid)
        {
            return _order.IsExistOrderFromUserFromToday(userid);
        }

        public void IsfinallyForOredr(Orders orders)
        {
            orders.IsFinally = true;
            orders.OrderState = OrderState.InProccess;

            _order.UpdateOrder(orders);
        }

        public void MinusProductToTheOrderDetails(int orderdetailid)
        {
            OrderDetails orderDetails = _order.GetOrderDetailByID(orderdetailid);
            orderDetails.Count = orderDetails.Count - 1;

            _order.UpdateOrderDetail(orderDetails);
        }

        //Get Order By Order Detail Id 
        public Orders GetOrderByOrderDetailId(int orderDetailId)
        {
            return _order.GetOrderByOrderDetailId(orderDetailId);
        }

        public void PlusProductToTheOrderDetails(int orderdetailid)
        {
            OrderDetails orderDetails = _order.GetOrderDetailByID(orderdetailid);
            orderDetails.Count = orderDetails.Count + 1;

            _order.UpdateOrderDetail(orderDetails);
        }

        public void RemoveProductFromShopCart(int orderdetailid)
        {
            OrderDetails orderDetails = _order.GetOrderDetailByID(orderdetailid);

            _order.RemoveProductFromShopCart(orderDetails);
        }

        public void ReturnedProduct(OrderDetails orderDetails)
        {
            orderDetails.IsReturend = true;
            _order.UpdateOrderDetail(orderDetails);
        }

        public Orders UpdateOrderByLocationid(Orders orders, int locationid)
        {
            orders.LocationID = locationid;
            _order.UpdateOrderByLocationid(orders);

            return orders;
        }

        //Fill Invoice Site Side ViewModel
        public async Task<InvoiceSiteSideViewModel> FillInvoiceSiteSideViewModel(int userId)
        {
            return await _order.FillInvoiceSiteSideViewModel(userId);
        }


        //Check That Is Exist Any Current Order Detail By This Product Id And User Id
        public async Task<bool> CheckThatIsExistAnyCurrentOrderDetailByThisProductIdAndUserId(int userId, int productId)
        {
            return await _order.CheckThatIsExistAnyCurrentOrderDetailByThisProductIdAndUserId(userId, productId);
        }

        //Fill List Of Order Details Admin Side View Model
        public async Task<List<ListOfOrderDetailsAdminSideViewModel>> FillListOfOrderDetailsAdminSideViewModel(int id)
        {
            return await _order.FillListOfOrderDetailsAdminSideViewModel(id);
        }

        //Delete User Order 
        public async Task<bool> DeleteUserOrder(int orderId, int UserId)
        {
            return await _order.DeleteUserOrder(orderId, UserId);
        }

        //Is Order In Last Step Of Shoping
        public async Task<bool> IsOrderInLastStepOfShoping(int ordeId, int userId)
        {
            return await _order.IsOrderInLastStepOfShoping(ordeId, userId);
        }

        #endregion

        #region Admin Side 

        //Check For Send Order To The Customer
        public async Task<bool> CheckForSendOrderToTheCustomer(int orderId)
        {
            //Get Order By Id 
            var order = _order.GetOrderByOrderID(orderId);
            if (order == null || order.OrderState != OrderState.InProccess) return false;

            #region Update Order State 

            order.OrderState = OrderState.SentToTheCustomer;

            _order.UpdateOrder(order);

            #endregion

            return true;
        }

        #endregion
    }
}
