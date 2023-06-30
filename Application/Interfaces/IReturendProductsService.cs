using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReturendProductsService
    {
        List<ReturnedProducts> GetAllReturendProductsByUserid(int userid);
        void AddRequestForReturnedProduct(ReturnedProducts products , int userid);
        List<ReturnedProducts> GetAllReturnedProducts();
        ReturnedProducts GetReturendProductByID(int id);
        void DeclineReturnRequest(ReturnedProducts returned);
        void AcceptReturnRequest(ReturnedProducts returned);
    }
}
