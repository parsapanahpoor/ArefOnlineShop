using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IReturendProductsRepository
    {
        List<ReturnedProducts> GetAllReturendProductsByUserid(int userid);
        List<ReturnedProducts> GetAllReturnedProducts();
        void SaveChanges();
        void AddRequestForReturnedProduct(ReturnedProducts products);
        ReturnedProducts GetReturendProductByID(int id);
        void UpdateReturendProduct(ReturnedProducts returned);
    }
}
