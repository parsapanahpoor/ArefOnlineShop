#region Using

using Domain.Models.Product;
using Domain.Models.Users;
using Domain.ViewModels.SiteSide.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Application.Interfaces
{
    public interface IProductService
    {
        #region ProductCategories

        List<ProductCategories> GetAllProductCategories();
        void AddProductCategories(ProductCategories productCategories, IFormFile? imageName);
        ProductCategories GetProductCatgeoriesById(int id);
        void UpdateProductCategories(ProductCategories productCategories, int id, IFormFile? imgBlogUp);
        void DeleteProductCategories(int id);

        #endregion

        #region Product

        List<Product> GetAllProducts();
        int AddProduct(Product product, IFormFile imgProductUp, User user);
        void AddCategoryToProduct(List<int> Categories, int ProductID);
        Product GetProductByID(int productid);
        List<ProductSelectedCategory> GetAllProductSelectedCategories();
        int UpdateProduct(Product product, IFormFile imgProductUp);
        void EditProductSelectedCategory(List<int> Categories, int productid);
        void DeleteProduct(Product product);
        List<Product> GetAllDeletedProducts();
        void UpdateProductForLock(Product product);
        Tuple<List<Product>, int> GetProductsForShowInHomePage(int? Categroyid, int pageId = 1, string filter = "", int take = 0, string orderByType = "date");
        Product GetProductForShowInSingleProducPage(int id);
        bool IsExistPRoduct(int productid);
        void MinusProductCountAfterSale(int productid, int count);
        List<Product> GetLastestProductsIndexPage();
        List<Product> GetLastestProductsIndexPagefor4Product();


        #endregion

        #region ProductFeatures

        List<ProductFeature> GetProductFeaturs(int id);
        void AddFeatureToProduct(ProductFeature feature);
        ProductFeature GetFeatureById(int id);
        void DeleteProductFeature(ProductFeature feature);
        List<ProductGallery> GetGalleryById(int id);
        void AddImageToGalleryProduct(ProductGallery productGallery, IFormFile imgUp);
        ProductGallery GetProductGalleryByID(int id);
        void DeleteProductGallery(ProductGallery product);
        void DeleteAllProductGAlleries(List<ProductGallery> productGalleries);
        #endregion

        #region Offer

        List<Product> GetAllProductsInOffer();
        List<Product> GetAllProductsNotInOffer();
        void DeleteProductFromOffer(Product product);
        List<Product> GetLastestOfferProducts();

        #endregion

        #region Site Side 

        //Fill Product Detail Site Side View Model
        Task<ProductDetailSiteSideViewModel> FillProductDetailSiteSideViewModel(int id);

        #endregion
    }
}
