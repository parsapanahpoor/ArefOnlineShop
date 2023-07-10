#region Usings

using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#endregion

namespace ParsaWorkShop.ViewComponents
{
    public class NewestProductsInProductPage : ViewComponent
    {
        #region Ctor

        private readonly IProductService _productService;

        public NewestProductsInProductPage(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            #region Categories

            var model = await _productService.FillSiteSideBar();

            #endregion


            return View("SiteSideBar", model);
        }
    }
}
