#region Usings

using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

namespace ParsaWorkShop.ViewComponents
{
    public class NewestProductsInProductPageViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IProductService _productService;

        public NewestProductsInProductPageViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            #region Categories

            var model = await _productService.FillNewest3Products();

            #endregion


            return View("NewestProductsInProductPage", model);
        }
    }
}
