using Data.Context;
using Domain.Interfaces;
using Domain.Models.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ReturendProductsReposiotry : IReturendProductsRepository
    {
        private ParsaWorkShopContext _context;
        public ReturendProductsReposiotry(ParsaWorkShopContext context)
        {
            _context = context;
        }

        public void AddRequestForReturnedProduct(ReturnedProducts products)
        {
            _context.ReturnedProducts.Add(products);
            SaveChanges();
        }

        public List<ReturnedProducts> GetAllReturendProductsByUserid(int userid)
        {
            return _context.ReturnedProducts.Where(p => p.UserId == userid)
                             .Include(p => p.OrderDetails).ThenInclude(p => p.Product).ToList();
        }

        public List<ReturnedProducts> GetAllReturnedProducts()
        {
            return _context.ReturnedProducts.Include(p=>p.Users).Include(p => p.OrderDetails).ThenInclude(p => p.Product).ToList();
        }

        public ReturnedProducts GetReturendProductByID(int id)
        {
            return _context.ReturnedProducts.Where(p => p.ReturnedProductID == id)
                        .Include(p => p.Users).Include(p => p.OrderDetails).ThenInclude(p => p.Product).Single();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UpdateReturendProduct(ReturnedProducts returned)
        {
            _context.ReturnedProducts.Update(returned);
            SaveChanges();
        }
    }
}
