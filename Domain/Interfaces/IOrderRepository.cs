using Domain.Models.Order;
using Domain.Models.Users;
using Domain.ViewModels.Admin.Order;
using Domain.ViewModels.SiteSide.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {

        #region Order
        void UpdateOrder(Orders orders);
        bool IsExistOrderFromUserFromToday(int userid);
        Orders GetOrderForShopCart(int userid);
        void AddOrderToTheShopCart(Orders orders);
        Orders GetOrderByOrderID(int orderid);
        void UpdateOrderByLocationid(Orders orders);
        Locations GetUserLocationByOrderId(int orderid);
        List<Orders> GetAllOrdersForShowInAdminPanel();
        Locations GetUserLocationByOrderID(int orderid);
        bool IsExistLocationInOrder(int Locationid);
        List<Orders> GetOrdersByUsersId(int userid);
        #endregion


        #region OrderDetails

        bool IsExistOrderDetailFromUserFromToday(int orderid, int productid, int colorId, int sizeId);
        OrderDetails AddOneMoreProductToTheShopCart(int orderid, int productid, int colorId, int sizeId);
        void UpdateOrderDetail(OrderDetails orderDetails);
        void AddOrderDetails(OrderDetails orderDetails);
        List<OrderDetails> GetAllOrderDetailsByOrderID(int orderid);
        void RemoveProductFromShopCart(OrderDetails orderDetails);
        OrderDetails GetOrderDetailByID(int orderdetailid);
        void SaveChanges();

        //Fill Invoice Site Side ViewModel
        Task<InvoiceSiteSideViewModel> FillInvoiceSiteSideViewModel(int userId);

        //Fill List Of Order Details Admin Side View Model
        Task<List<ListOfOrderDetailsAdminSideViewModel>> FillListOfOrderDetailsAdminSideViewModel(int id);

        #endregion
    }
}
