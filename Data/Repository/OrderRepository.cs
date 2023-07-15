using Data.Context;
using Domain.Interfaces;
using Domain.Models.Order;
using Domain.Models.Users;
using Domain.ViewModels.Admin.Order;
using Domain.ViewModels.SiteSide.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                                                                                            ColorName = c.ColorTitle,
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
                                 .Where(p => p.OrderDetailID == id)
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
    }
}
