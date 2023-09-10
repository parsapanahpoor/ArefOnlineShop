using Data.Context;
using Domain.Interfaces;
using Domain.Models.Order;
using Domain.Models.Users;
using Domain.ViewModels.Admin.Order;
using Domain.ViewModels.SiteSide.Order;
using Domain.ViewModels.UserPanel.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private ParsaWorkShopContext _context;
        public OrderRepository(ParsaWorkShopContext context)
        {
            _context = context;
        }

        public OrderDetails AddOneMoreProductToTheShopCart(int orderid, int productid, int colorId, int sizeId)
        {
            return _context.OrderDetails
                           .FirstOrDefault(p => p.OrderID == orderid && p.ProductID == productid
                                           && p.ColorId == colorId && p.SizeId == sizeId);
        }

        public void AddOrderDetails(OrderDetails orderDetails)
        {
            _context.OrderDetails.Add(orderDetails);
            SaveChanges();
        }

        public void AddOrderToTheShopCart(Orders orders)
        {
            _context.Orders.Add(orders);
            SaveChanges();
        }

        public List<OrderDetails> GetAllOrderDetailsByOrderID(int orderid)
        {
            return _context.OrderDetails.Where(p => p.OrderID == orderid)
                                        .Include(p => p.Product).ToList();            
        }

        //Fill List Of User Orders Details User Side View Model
        public async Task<List<ListOfUserOrdersDetailsUserSideViewModel>> FillListOfUserOrdersDetailsUserSideViewModel(int orderId)
        {
            return await _context.OrderDetails
                                 .Include(p=> p.Product)
                                 .AsNoTracking()
                                 .Where(p=> p.OrderID == orderId)
                                 .Select(p=> new ListOfUserOrdersDetailsUserSideViewModel()
                                 {
                                     OrderDetails = p,
                                     Locations = _context.Orders
                                                         .Include(s=> s.Locations)
                                                         .AsNoTracking()
                                                         .Where(s => s.IsFinally && s.OrderId == orderId)
                                                         .Select(s=> s.Locations)
                                                         .FirstOrDefault(),
                                     Product = p.Product,
                                     ProductColor = _context.ProductColors
                                                            .AsNoTracking()      
                                                            .FirstOrDefault(s=> s.Id == p.ColorId),
                                     ProductsSize = _context.ProductsSizes
                                                            .AsNoTracking()
                                                            .FirstOrDefault(s => s.Id == p.SizeId),
                                     Order = _context.Orders
                                                     .Include(s=> s.User)
                                                     .AsNoTracking()
                                                     .FirstOrDefault(s=> s.OrderId == orderId)
                                 })
                                 .ToListAsync();
        }

        public List<Orders> GetAllOrdersForShowInAdminPanel()
        {
            return _context.Orders.Where(p => p.IsFinally == true)
                            .Include(p => p.User).Include(p => p.OrderDetails).ToList();
        }

        public Orders GetOrderByOrderID(int orderid)
        {
            return _context.Orders.Find(orderid);
        }

        public OrderDetails GetOrderDetailByID(int orderdetailid)
        {
            return _context.OrderDetails.Find(orderdetailid);
        }

        public Orders GetOrderForShopCart(int userid)
        {
            return _context.Orders.SingleOrDefault(p => p.Userid == userid && p.CreateDate.Year == DateTime.Now.Year
                                   && p.CreateDate.Month == DateTime.Now.Month && p.CreateDate.Day == DateTime.Now.Day
                                   && p.IsFinally == false);
        }

        public List<Orders> GetOrdersByUsersId(int userid)
        {
            return _context.Orders.Where(p => p.IsFinally == true && p.Userid == userid)
                                        .Include(p => p.OrderDetails).ThenInclude(p=> p.Product)
                                        .Include(p => p.Locations)
                                        .OrderByDescending(p=> p.CreateDate)
                                        .ToList();
        }

        public Locations GetUserLocationByOrderId(int orderid)
        {
            return _context.Orders.Where(p => p.OrderId == orderid)
                                           .Include(p => p.Locations).Select(p => p.Locations).Single();
        }

        public Locations GetUserLocationByOrderID(int orderid)
        {
            return _context.Orders.Where(p => p.OrderId == orderid)
                                    .Include(p => p.Locations).Select(p => p.Locations).Single();
        }

        public bool IsExistLocationInOrder(int Locationid)
        {
            return _context.Orders.Any(p => p.LocationID == Locationid);
        }

        public bool IsExistOrderDetailFromUserFromToday(int orderid, int productid, int colorId, int sizeId)
        {
            return _context.OrderDetails
                           .Any(p => p.OrderID == orderid && p.ProductID == productid && p.ColorId == colorId && p.SizeId == sizeId);
        }

        public bool IsExistOrderFromUserFromToday(int userid)
        {
            return _context.Orders.Any(p => p.Userid == userid && p.CreateDate.Year == DateTime.Now.Year
                                   && p.CreateDate.Month == DateTime.Now.Month && p.CreateDate.Day == DateTime.Now.Day
                                   && p.IsFinally == false);
        }

        public void RemoveProductFromShopCart(OrderDetails orderDetails)
        {
            _context.OrderDetails.Remove(orderDetails);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UpdateOrder(Orders orders)
        {
            _context.Orders.Update(orders);
            SaveChanges();
        }

        public void UpdateOrderByLocationid(Orders orders)
        {
            _context.Orders.Update(orders);
            SaveChanges();
        }

        public void UpdateOrderDetail(OrderDetails orderDetails)
        {
            _context.OrderDetails.Update(orderDetails);
            SaveChanges();
        }

        //Check That Is Exist Any Current Order Detail By This Product Id And User Id
        public async Task<bool> CheckThatIsExistAnyCurrentOrderDetailByThisProductIdAndUserId(int userId , int productId)
        {
            var order = await _context.Orders
                           .Where(p => p.Userid == userId && p.CreateDate.Year == DateTime.Now.Year
                                                   && p.CreateDate.Month == DateTime.Now.Month && p.CreateDate.Day == DateTime.Now.Day
                                                   && p.IsFinally == false)
                           .FirstOrDefaultAsync();

            if (order == null) return false;

            return await _context.OrderDetails.AnyAsync(p => p.OrderID == order.OrderId
                                                        && p.ProductID == productId);
        }

        //Fill Invoice Site Side ViewModel
        public async Task<InvoiceSiteSideViewModel> FillInvoiceSiteSideViewModel(int userId)
        {
            return await _context.Orders
                           .Where(p => p.Userid == userId && p.CreateDate.Year == DateTime.Now.Year
                                                   && p.CreateDate.Month == DateTime.Now.Month && p.CreateDate.Day == DateTime.Now.Day
                                                   && p.IsFinally == false)
                           .Select(p => new InvoiceSiteSideViewModel()
                           {
                               Order = p,
                               InvoiceOrderDetails = _context.OrderDetails
                                                             .Where(s => s.OrderID == p.OrderId)
                                                             .Select(s => new InvoiceOrderDetailSiteSideViewModel()
                                                             {
                                                                 Count = s.Count,
                                                                 OrderDetailID = s.OrderDetailID,
                                                                 InvoiceColor = _context.ProductColors
                                                                                        .Where(c => !c.IsDelete && c.Id == s.ColorId)
                                                                                        .Select(c => new InvoiceColorSiteSideViewModel()
                                                                                        {
                                                                                            ColorId = c.Id,
                                                                                            ColorName = c.ColorFarsiTitle,
                                                                                            ColorCode = c.ColorCode
                                                                                        })
                                                                                        .FirstOrDefault(),
                                                                 InvoiceSize = _context.ProductsSizes
                                                                                       .Where(size => !size.IsDelete && size.Id == s.SizeId)
                                                                                       .Select(size => new InvoiceSizeSiteSideViewModel()
                                                                                       {
                                                                                           SizeId = size.Id,
                                                                                           SizeName = size.SizeTitle
                                                                                       })
                                                                                       .FirstOrDefault(),
                                                                 Product = _context.product
                                                                                   .Where(pro => !pro.IsDelete && pro.ProductID == s.ProductID)
                                                                                   .Select(pro => new InvoiceProductSiteSideViewModel()
                                                                                   {
                                                                                       Price = pro.Price,
                                                                                       ProductId = pro.ProductID,
                                                                                       ProductImage = pro.ProductImageName,
                                                                                       ProductTitle = pro.ProductTitle,
                                                                                   })
                                                                                   .FirstOrDefault(),
                                                             })
                                                             .ToList()
                           })
                           .FirstOrDefaultAsync();
        }

        //Fill List Of Order Details Admin Side View Model
        public async Task<List<ListOfOrderDetailsAdminSideViewModel>> FillListOfOrderDetailsAdminSideViewModel(int id)
        {
            return await _context.OrderDetails
                                 .AsNoTracking()
                                 .Include(p=> p.Product)
                                 .Where(p => p.OrderID == id)
                                 .Select(p => new ListOfOrderDetailsAdminSideViewModel()
                                 {
                                     OrderDetails = p,
                                     ProductColor = _context.ProductColors
                                                            .AsNoTracking()
                                                            .Where(s => !s.IsDelete && s.Id == p.ColorId)
                                                            .FirstOrDefault(),
                                     ProductsSize = _context.ProductsSizes
                                                            .AsNoTracking()
                                                            .Where(s => !s.IsDelete && s.Id == p.SizeId)
                                                            .FirstOrDefault(),
                                 })
                                 .ToListAsync(); 
        }

        //Delete User Order 
        public async Task<bool> DeleteUserOrder(int orderId , int UserId)
        {
            #region Get Order With Datas

            var order = await _context.Orders
                                      .FirstOrDefaultAsync(p=> !p.IsFinally && p.Userid == UserId && p.OrderId == orderId && p.Price.HasValue); 

            if(order == null) return false;

            #endregion

            #region Remove Order Detail And Discount  

            var orderDetails = await _context.OrderDetails
                                             .Where(p => p.OrderID == orderId)
                                             .ToListAsync();

            if (orderDetails != null && orderDetails.Any())
            {
                    _context.OrderDetails.RemoveRange(orderDetails);
            }

            #endregion

            #region Discount 

            var discount = await _context.DiscountCodeSelectedUsers
                                         .FirstOrDefaultAsync(p => !p.IsDelete && p.UserId == UserId && p.OrderId == orderId);

            if (discount != null)
            {
                _context.DiscountCodeSelectedUsers.Remove(discount);
            }

            //Update Order
            order.Price = null;
            _context.Orders.Update(order);

            //Save Changes
            await _context.SaveChangesAsync();

            #endregion

            return true;
        }

        //Is Order In Last Step Of Shoping
        public async Task<bool> IsOrderInLastStepOfShoping(int ordeId, int userId)
        {
            return await _context.Orders
                                 .AnyAsync(p=> !p.IsFinally && p.OrderId == ordeId && p.Userid == userId && p.Price.HasValue);
        }

        //Get Order By Order Detail Id 
        public Orders GetOrderByOrderDetailId(int orderDetailId)
        {
            return _context.OrderDetails
                           .Include(p=> p.Order)
                           .Where(p=> p.OrderDetailID == orderDetailId)
                           .Select(p=> p.Order)
                           .FirstOrDefault();
        }

        #region Admin Side 

        //Get List Of In Progress Orders
        public async Task<List<Orders>> GetListOfInProgressOrders()
        {
            return await _context.Orders
                                 .Where(p=> p.IsFinally && p.OrderState == OrderState.InProccess)
                                 .ToListAsync(); 
        }

        #endregion
    }
}
