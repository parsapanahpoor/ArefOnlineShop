using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.ViewComponents
{
    public class Lastest4ProductsComponent : ViewComponent
    {
        private IProductService _product;
        public Lastest4ProductsComponent(IProductService product)
        {
            _product = product;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("Lastest4ProductsComponent", _product.GetLastestProductsIndexPagefor4Product()));
        }
    }
}
