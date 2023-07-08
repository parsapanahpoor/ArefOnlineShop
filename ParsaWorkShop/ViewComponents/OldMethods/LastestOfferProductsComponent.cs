using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaWorkShop.ViewComponents
{
    public class LastestOfferProductsComponent : ViewComponent
    {
        private IProductService _product;
        public LastestOfferProductsComponent(IProductService product)
        {
            _product = product;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LastestOfferProductsComponent", _product.GetLastestOfferProducts()));
        }
    }
}
