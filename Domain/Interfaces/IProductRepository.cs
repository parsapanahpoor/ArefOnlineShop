using Domain.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {

        #region ProductCategories

        List<ProductCategories> GetAllProductCategories();
        void AddProductCategories(ProductCategories productCategories);
        ProductCategories GetProductCatgeoriesById(int id);
        void UpdateProductCategories(ProductCategories productCategories);
        void Savechanges();

        #endregion


        #region Product

        List<Product> GetAllProducts();
        void AddProduct(Product product);
        void AddCategoryToProduct(ProductSelectedCategory product);
        Product GetProductByID(int productid);
        List<ProductSelectedCategory> GetAllProductSelectedCategories();
        void UpdateProduct(Product product);
        void EditProductSelectedCategory( int productid);
        List<Product> GetAllDeletedProducts();
        IQueryable<Product> GetAllProductForIQueryAble();
        IQueryable<Product> SearchForProducts(string filter);
        IQueryable<Product> GetProductsHaveThisCategory(int category);
        Product GetProductForShowInSingleProducPage(int id);
        bool IsExistPRoduct(int productid);
        List<Product> GetLastestProductsIndexPageUnder8();
        List<Product> GetLastestProductsIndexPageUnder4();
        List<Product> GetLastestProductsIndexPageUpper8();
        List<Product> GetLastestProductsIndexPageUpper4();
        int ProductCount();

        #endregion


        #region ProductFeatures

        List<ProductFeature> GetProductFeaturs(int id);
        void AddFeatureToProduct(ProductFeature feature);
        ProductFeature GetFeatureById(int id);
        void DeleteProductFeature(ProductFeature feature);
        List<ProductGallery> GetGalleryById(int id);
        void AddProductGallery(ProductGallery product);
        ProductGallery GetProductGalleryByID(int id);
        void DeleteProductGallery(ProductGallery product);

        #endregion

        #region Offer

        List<Product> GetAllProductsInOffer();
        List<Product> GetAllProductsNotInOffer();
        List<Product> GetLastestOfferProducts();

        #endregion
    }
}
