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


        //Check That Is Exist Any Current Order Detail By This Product Id And User Id
        Task<bool> CheckThatIsExistAnyCurrentOrderDetailByThisProductIdAndUserId(int userId, int productId);

        bool IsExistOrderDetailFromUserFromToday(int orderid, int productid, int colorId, int sizeId);
        OrderDetails AddOneMoreProductToTheShopCart(int orderid, int productid, int colorId, int sizeId);
        void UpdateOrderDetail(OrderDetails orderDetails);
        void AddOrderDetails(OrderDetails orderDetails);
        List<OrderDetails> GetAllOrderDetailsByOrderID(int orderid);
        void RemoveProductFromShopCart(OrderDetails orderDetails);
        OrderDetails GetOrderDetailByID(int orderdetailid);
        void SaveChanges();

        //Fill List Of User Orders Details User Side View Model
        Task<List<ListOfUserOrdersDetailsUserSideViewModel>> FillListOfUserOrdersDetailsUserSideViewModel(int orderId);

        //Fill Invoice Site Side ViewModel
        Task<InvoiceSiteSideViewModel> FillInvoiceSiteSideViewModel(int userId);

        //Fill List Of Order Details Admin Side View Model
        Task<List<ListOfOrderDetailsAdminSideViewModel>> FillListOfOrderDetailsAdminSideViewModel(int id);

        //Delete User Order 
        Task<bool> DeleteUserOrder(int orderId, int UserId);

        //Is Order In Last Step Of Shoping
        Task<bool> IsOrderInLastStepOfShoping(int ordeId, int userId);

        //Get Order By Order Detail Id 
        Orders GetOrderByOrderDetailId(int orderDetailId);

        #endregion

        #region Admin Side 

        //Get List Of In Progress Orders
        Task<List<Orders>> GetListOfInProgressOrders();

        #endregion
    }
}
