using Domain.Models.Product;
using Domain.ViewModels.Admin.Product;
using Domain.ViewModels.SiteSide.Product;
using Domain.ViewModels.SiteSide.SitSideBar;
using Microsoft.EntityFrameworkCore;
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

        #region Site Side

        //Fill Product Detail Site Side View Model
        Task<ProductDetailSiteSideViewModel> FillProductDetailSiteSideViewModel(int id);

        //Check That Is Exist Product With This Color
        Task<bool> CheckThatIsExistProductWithThisColor(int productId, int colorId);

        //Check That Is Exist Product With This Size
        Task<bool> CheckThatIsExistProductWithThisSize(int productId, int sizeId);

        //List Of Product Categories For Show Site Side Bar
        Task<List<ListOfProductCategoriesForShowInSiteSideBar>> ListOfProductCategoriesForShowSiteSideBar();

        #endregion

        #region Admin Side 

        //Fill List Of Product Colors For Choose Admin Side View Model
        Task<List<ListOfProductColorsForChooseAdminSideViewModel>> FillListOfProductColorsForChooseAdminSideViewModel();

        //Fill List Of Product Size For Choose Admin Side View Model
        Task<List<ListOfProductSizesForChooseAdminSideViewModel>> FillListOfProductSizesForChooseAdminSideViewModel();

        //Add Product Selected Sizes Without Save Changes
        Task AddProductSelectedSizesWithoutSaveChanges(List<ProductSelectedSize> selectedSize);

        //Add Product Selected Color Without Save Changes
        Task AddProductSelectedColorWithoutSaveChanges(List<ProductSelectedColors> selectedColors);

        //Save Changes
        Task SaveChanges();

        //Get All Product Selected Size
        Task<List<int>> GetAllProductSelectedSize(int productId);

        //Get All Product Selected Color
        Task<List<int>> GetAllProductSelectedColor(int productId);

        //Update Product Color And Sizes
        Task UpdateProductColorAndSizes(int productId);

        //Get Product Selected Color By Product Id
        Task<List<ProductColor>> GetProductSelectedColorByProductId(int productId);

        //Get Product Selected Size By Product Id
        Task<List<ProductsSize>> GetProductSelectedSizeByProductId(int productId);

        #endregion
    }
}
