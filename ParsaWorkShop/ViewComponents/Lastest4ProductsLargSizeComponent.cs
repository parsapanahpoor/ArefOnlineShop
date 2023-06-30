using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.ViewComponents
{
    public class Lastest4ProductsLargSizeComponent : ViewComponent
    {
        private IProductService _product;
        public Lastest4ProductsLargSizeComponent(IProductService product)
        {
            _product = product;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("Lastest4ProductsLargSizeComponent", _product.GetLastestProductsIndexPagefor4Product()));
        }
    }
}
