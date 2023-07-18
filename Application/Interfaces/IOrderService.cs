using Domain.Models.Order;
using Domain.Models.Users;
using Domain.ViewModels.Admin.Order;
using Domain.ViewModels.SiteSide.Order;
using Domain.ViewModels.UserPanel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {

        #region Order

        bool IsExistOrderFromUserFromToday(int userid);
        Orders GetOrderForShopCart(int userid);
        int AddOrderToTheShopCart(int userid);
        Orders GetOrderByOrderID(int orderid);
        Orders UpdateOrderByLocationid(Orders orders, int locationid);
        Locations GetUserLocationByOrderId(int orderid);
        void IsfinallyForOredr(Orders orders);
        List<Orders> GetAllOrdersForShowInAdminPanel();
        Locations GetUserLocationByOrderID(int orderid);
        bool IsExistLocationInOrder(int Locationid);
        List<Orders> GetOrdersByUsersId(int userid);

        //Fill Invoice Site Side ViewModel
        Task<InvoiceSiteSideViewModel> FillInvoiceSiteSideViewModel(int userId);

        #endregion


        #region OrderDetails

        bool IsExistOrderDetailFromUserFromToday(int orderid, int productid , int colorId , int sizeId);
        void AddOneMoreProductToTheShopCart(int orderid, int productid, int colorId, int sizeId , int count);
        void AddProductToOrderDetail(int OrderID, int ProductID, decimal Price, int colorId, int sizeId, int count);
        List<OrderDetails> GetAllOrderDetailsByOrderID(int orderid);
        //Fill List Of User Orders Details User Side View Model
        Task<List<ListOfUserOrdersDetailsUserSideViewModel>> FillListOfUserOrdersDetailsUserSideViewModel(int orderId);
        bool CheckForProductCount(int Orderid);
        void RemoveProductFromShopCart(int orderdetailid);
        void PlusProductToTheOrderDetails(int orderdetailid);
        void MinusProductToTheOrderDetails(int orderdetailid);
        OrderDetails GetOrderDetailByID(int orderdetailid);
        decimal GetPriceOfOrderDetailByOrderDetailID(int OrderdetailID);
        void ReturnedProduct(OrderDetails orderDetails);

        //Fill List Of Order Details Admin Side View Model
        Task<List<ListOfOrderDetailsAdminSideViewModel>> FillListOfOrderDetailsAdminSideViewModel(int id);

        //Delete User Order 
        Task<bool> DeleteUserOrder(int orderId, int UserId);

        //Is Order In Last Step Of Shoping
        Task<bool> IsOrderInLastStepOfShoping(int ordeId, int userId);

        //Get Order By Order Detail Id 
        Orders GetOrderByOrderDetailId(int orderDetailId);

        #endregion
    }
}
