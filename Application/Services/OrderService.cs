using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models.Order;
using Domain.Models.Users;
using Domain.ViewModels.SiteSide.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _order;
        public OrderService(IOrderRepository order)
        {
            _order = order;
        }

        public void AddOneMoreProductToTheShopCart(int orderid, int productid, int colorId, int sizeId , int count)
        {
            OrderDetails orderDetails = _order.AddOneMoreProductToTheShopCart(orderid, productid , colorId , sizeId);
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

        public void AddProductToOrderDetail(int OrderID, int ProductID, decimal Price, int colorId, int sizeId , int count)
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

        public List<Orders> GetAllOrdersForShowInAdminPanel()
        {
            return _order.GetAllOrdersForShowInAdminPanel();
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
            return _order.IsExistOrderDetailFromUserFromToday(orderid, productid , colorId , sizeId);
        }

        public bool IsExistOrderFromUserFromToday(int userid)
        {
            return _order.IsExistOrderFromUserFromToday(userid);
        }

        public void IsfinallyForOredr(Orders orders)
        {
            orders.IsFinally = true;

            _order.UpdateOrder(orders);
        }

        public void MinusProductToTheOrderDetails(int orderdetailid)
        {
            OrderDetails orderDetails = _order.GetOrderDetailByID(orderdetailid);
            orderDetails.Count = orderDetails.Count - 1;

            _order.UpdateOrderDetail(orderDetails);
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
    }
}
