using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReturendProductsService : IReturendProductsService
    {
        private IReturendProductsRepository _returnProduct;
        public ReturendProductsService(IReturendProductsRepository returnProduct)
        {
            _returnProduct = returnProduct;
        }

        public void AcceptReturnRequest(ReturnedProducts returned)
        {
            returned.ReturnProductTypeID = 2;
            _returnProduct.UpdateReturendProduct(returned);
        }

        public void AddRequestForReturnedProduct(ReturnedProducts products , int userid)
        {
            products.UserId = userid;
            products.CreateDate = DateTime.Now;
            products.ReturnProductTypeID = 1;

            _returnProduct.AddRequestForReturnedProduct(products);
        }

        public void DeclineReturnRequest(ReturnedProducts returned)
        {
            returned.ReturnProductTypeID = 3;
            _returnProduct.UpdateReturendProduct(returned);
        }

        public List<ReturnedProducts> GetAllReturendProductsByUserid(int userid)
        {
            return _returnProduct.GetAllReturendProductsByUserid(userid);
        }

        public List<ReturnedProducts> GetAllReturnedProducts()
        {
            return _returnProduct.GetAllReturnedProducts();
        }

        public ReturnedProducts GetReturendProductByID(int id)
        {
            return _returnProduct.GetReturendProductByID(id);
        }
    }
}
