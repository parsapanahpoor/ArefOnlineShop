using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.ViewComponents
{
    public class LastestProductsComponent : ViewComponent
    {
        private IProductService _product;
        public LastestProductsComponent(IProductService product)
        {
            _product = product;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LastestProductsComponent", _product.GetLastestProductsIndexPage()));
        }
    }
}
