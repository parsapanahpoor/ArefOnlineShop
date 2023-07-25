#region Using

using Application.Convertors;
using Application.Extensions;
using Application.Genarator;
using Application.Interfaces;
using Application.Security;
using Application.StaticTools;
using Domain.Interfaces;
using Domain.Models.Product;
using Domain.Models.Slider;
using Domain.Models.Users;
using Domain.ViewModels.Admin.Product;
using Domain.ViewModels.SiteSide.Home;
using Domain.ViewModels.SiteSide.Product;
using Domain.ViewModels.SiteSide.SitSideBar;
using Domain.ViewModels.UserPanel.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace Application.Services
{
    public class ProductService : IProductService
    {
        #region Ctor

        private IProductRepository _product;

        public ProductService(IProductRepository product)
        {
            _product = product;
        }

        #endregion

        #region OLd Methods

        public void AddCategoryToProduct(List<int> Categories, int ProductID)
        {
            foreach (var item in Categories)
            {
                ProductSelectedCategory product = new ProductSelectedCategory()
                {
                    ProductCategoryId = item,
                    ProductID = ProductID
                };

                _product.AddCategoryToProduct(product);
            }
        }

        public void AddFeatureToProduct(ProductFeature feature)
        {
            _product.AddFeatureToProduct(feature);
        }

        public void AddImageToGalleryProduct(ProductGallery productGallery, IFormFile imgUp)
        {

            productGallery.ImageName = "no-photo.png";  //تصویر پیشفرض
            //TODO Check Image
            if (imgUp != null && imgUp.IsImage())
            {
                productGallery.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product/ProducGallery", productGallery.ImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgUp.CopyTo(stream);
                }
            }

            _product.AddProductGallery(productGallery);
        }

        public int AddProduct(Product product, IFormFile imgProductUp, User user)
        {

            product.UserId = user.UserId;
            product.IsActive = true;
            product.CreateDate = DateTime.Now;
            product.ProductImageName = "no-photo.png";  //تصویر پیشفرض

            if (imgProductUp != null && imgProductUp.IsImage())
            {
                product.ProductImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgProductUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product/image", product.ProductImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgProductUp.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product/thumb", product.ProductImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 320);
            }

            _product.AddProduct(product);

            return product.ProductID;
        }

        public void AddProductCategories(ProductCategories productCategories, IFormFile? catimageName)
        {
            ProductCategories cat = new ProductCategories();
            cat.CategoryTitle = productCategories.CategoryTitle;
            cat.IsDelete = false;
            cat.ParentId = productCategories.ParentId;
            cat.PTagClass = productCategories.PTagClass;
            cat.DivTagClass = productCategories.DivTagClass;
            cat.Priority = productCategories.Priority;
            cat.ShowInPart1 = productCategories.ShowInPart1;
            cat.ShowInPart2 = productCategories.ShowInPart2;

            #region Add Slider Image

            if (catimageName != null)
            {
                var imageName = Guid.NewGuid() + Path.GetExtension(catimageName.FileName);
                catimageName.AddImageToServer(imageName, PathTools.ProductCategoryPathServer, 400, 300, PathTools.ProductCategoryPathThumbServer);

                cat.ImageName = imageName;
            }

            #endregion

            _product.AddProductCategories(cat);
        }

        public void DeleteAllProductGAlleries(List<ProductGallery> productGalleries)
        {
            foreach (var item in productGalleries)
            {
                string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product/ProducGallery", item.ImageName);
                if (File.Exists(deleteimagePath))
                {
                    File.Delete(deleteimagePath);
                }
                _product.DeleteProductGallery(item);
            }
        }

        public void DeleteProduct(Product product)
        {
            if (product.ProductImageName != "no-photo.png")
            {
                string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product/image", product.ProductImageName);
                if (File.Exists(deleteimagePath))
                {
                    File.Delete(deleteimagePath);
                }

                string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images//Product/thumb", product.ProductImageName);
                if (File.Exists(deletethumbPath))
                {
                    File.Delete(deletethumbPath);
                }
            }
            product.ProductImageName = "no-photo.png";
            product.IsDelete = true;

            List<ProductGallery> productGalleries = GetGalleryById(product.ProductID);
            DeleteAllProductGAlleries(productGalleries);

            _product.UpdateProduct(product);
        }

        public void DeleteProductCategories(int id)
        {
            ProductCategories productCategories = GetProductCatgeoriesById(id);
            productCategories.IsDelete = true;


            if (!string.IsNullOrEmpty(productCategories.ImageName))
            {
                productCategories.ImageName.DeleteImage(PathTools.ProductCategoryPathServer, PathTools.ProductCategoryPathThumbServer);
            }

            _product.UpdateProductCategories(productCategories);
        }

        public void DeleteProductFeature(ProductFeature feature)
        {
            _product.DeleteProductFeature(feature);
        }

        public void DeleteProductFromOffer(Product product)
        {
            product.Price = product.OldPrice.Value;
            product.OldPrice = null;
            product.IsInOffer = null;
            product.OfferPercent = null;

            _product.UpdateProduct(product);
        }

        public void DeleteProductGallery(ProductGallery product)
        {
            string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product/ProducGallery", product.ImageName);
            if (File.Exists(deleteimagePath))
            {
                File.Delete(deleteimagePath);
            }
            _product.DeleteProductGallery(product);
        }

        public void EditProductSelectedCategory(List<int> Categories, int productid)
        {
            _product.EditProductSelectedCategory(productid);
            AddCategoryToProduct(Categories, productid);
        }

        public List<Product> GetAllDeletedProducts()
        {
            return _product.GetAllDeletedProducts();
        }

        public List<ProductCategories> GetAllProductCategories()
        {
            return _product.GetAllProductCategories();
        }

        public List<Product> GetAllProducts()
        {
            return _product.GetAllProducts();
        }

        public List<ProductSelectedCategory> GetAllProductSelectedCategories()
        {
            return _product.GetAllProductSelectedCategories();
        }

        public List<Product> GetAllProductsInOffer()
        {
            return _product.GetAllProductsInOffer();
        }

        public List<Product> GetAllProductsNotInOffer()
        {
            return _product.GetAllProductsNotInOffer();
        }

        public ProductFeature GetFeatureById(int id)
        {
            return _product.GetFeatureById(id);
        }

        public List<ProductGallery> GetGalleryById(int id)
        {
            return _product.GetGalleryById(id);
        }

        public List<Product> GetLastestOfferProducts()
        {
            return _product.GetLastestOfferProducts();
        }

        public List<Product> GetLastestProductsIndexPage()
        {
            if (_product.ProductCount() >= 8)
            {
                return _product.GetLastestProductsIndexPageUpper8();
            }

            return _product.GetLastestProductsIndexPageUnder8();
        }

        public List<Product> GetLastestProductsIndexPagefor4Product()
        {
            if (_product.ProductCount() >= 4)
            {
                return _product.GetLastestProductsIndexPageUpper4();
            }

            return _product.GetLastestProductsIndexPageUnder4();
        }

        public Product GetProductByID(int productid)
        {
            return _product.GetProductByID(productid);
        }

        public ProductCategories GetProductCatgeoriesById(int id)
        {
            return _product.GetProductCatgeoriesById(id);
        }

        public List<ProductFeature> GetProductFeaturs(int id)
        {
            return _product.GetProductFeaturs(id);
        }

        public Product GetProductForShowInSingleProducPage(int id)
        {
            return _product.GetProductForShowInSingleProducPage(id);
        }

        public ProductGallery GetProductGalleryByID(int id)
        {
            return _product.GetProductGalleryByID(id);
        }

        public Tuple<List<Product>, int> GetProductsForShowInHomePage(int? Categroyid, int pageId = 1, string filter = "", int take = 0, string orderByType = "date")
        {
            if (take == 0) take = 15;

            IQueryable<Product> products = _product.GetAllProductForIQueryAble();

            if (!string.IsNullOrEmpty(filter))
            {
                products = _product.SearchForProducts(filter);
            }

            if (Categroyid != null)
            {
                IQueryable<Product> productid = _product.GetProductsHaveThisCategory((int)Categroyid);

                if (Categroyid != null)
                {
                    if (productid.Any() && productid != null)
                    {
                        products = productid;
                    }
                }
            }

            switch (orderByType)
            {
                case "date":
                    {
                        products = products.OrderByDescending(c => c.CreateDate);
                        break;
                    }
                case "LowPrice":
                    {
                        products = products.OrderBy(c => c.Price);
                        break;
                    }
                case "HighPrice":
                    {
                        products = products.OrderByDescending(c => c.Price);
                        break;
                    }
                case "OfferProduct":
                    {
                        products = products.OrderByDescending(c => c.IsInOffer);
                        break;
                    }
            }

            int skip = (pageId - 1) * take;
            int pageCount = (products.Count() / take);

            if ((pageCount % 2) == 0 || (pageCount % 2) != 0)
            {
                pageCount += 1;
            }

            var query = products.Skip(skip).Take(take).ToList();

            return Tuple.Create(query, pageCount);
        }

        public bool IsExistPRoduct(int productid)
        {
            return _product.IsExistPRoduct(productid);
        }

        public void MinusProductCountAfterSale(int productid, int count)
        {
            var product = GetProductByID(productid);
            product.ProductCount = product.ProductCount - count;

            _product.UpdateProduct(product);
        }

        public int UpdateProduct(Product product, IFormFile imgProductUp)
        {

            if (imgProductUp != null && imgProductUp.IsImage())
            {

                if (product.ProductImageName != "no-photo.png")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Product/image", product.ProductImageName);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }

                    string deletethumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images//Product/thumb", product.ProductImageName);
                    if (File.Exists(deletethumbPath))
                    {
                        File.Delete(deletethumbPath);
                    }
                }



                product.ProductImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(imgProductUp.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images//Product/image", product.ProductImageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imgProductUp.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images//Product/thumb", product.ProductImageName);

                imgResizer.Image_resize(imagePath, thumbPath, 320);
            }

            _product.UpdateProduct(product);

            return product.ProductID;
        }

        public void UpdateProductCategories(ProductCategories productCategories, int id, IFormFile? imgBlogUp)
        {
            ProductCategories category = GetProductCatgeoriesById(id);
            category.CategoryTitle = productCategories.CategoryTitle;
            category.DivTagClass = productCategories.DivTagClass;
            category.Priority = productCategories.Priority;
            category.PTagClass = productCategories.PTagClass;
            category.ShowInPart1 = productCategories.ShowInPart1;
            category.ShowInPart2 = productCategories.ShowInPart2;

            #region Update Image

            if (imgBlogUp != null)
            {
                var imageName = Guid.NewGuid() + Path.GetExtension(imgBlogUp.FileName);
                imgBlogUp.AddImageToServer(imageName, PathTools.ProductCategoryPathServer, 400, 300, PathTools.ProductCategoryPathThumbServer);

                if (!string.IsNullOrEmpty(category.ImageName))
                {
                    category.ImageName.DeleteImage(PathTools.ProductCategoryPathServer, PathTools.ProductCategoryPathThumbServer);
                }

                category.ImageName = imageName;
            }

            #endregion

            _product.UpdateProductCategories(category);
        }

        public void UpdateProductForLock(Product product)
        {
            _product.UpdateProduct(product);
        }

        #endregion

        #region Site Side 

        //Get Product Name By Product Id
        public async Task<string> GetProductNameByProductId(int productId)
        {
            return await _product.GetProductNameByProductId(productId);
        }

        //User Panel Dashboard View Model
        public async Task<UserPanelDashboardViewModel> UserPanelDashboardViewModel(int userId)
        {
            return await _product.UserPanelDashboardViewModel(userId);
        }

        //Get Product Title With Product Id
        public async Task<string> GetProductTitleWithProductId(int id)
        {
            return await _product.GetProductTitleWithProductId(id);
        }

        //Fill Product Detail Site Side View Model
        public async Task<ProductDetailSiteSideViewModel> FillProductDetailSiteSideViewModel(int id)
        {
            var returnModel = await _product.FillProductDetailSiteSideViewModel(id);

            #region Color

            returnModel.ProductColors = await _product.GetProductSelectedColorByProductId(id);

            #endregion

            #region Sizes

            returnModel.ProductsSizes = await _product.GetProductSelectedSizeByProductId(id);

            #endregion

            #region Product Categories 

            returnModel.ProductCategoryLinkable = await _product.FillProductCategoryLinkable(id);

            #endregion

            #region Comments

            returnModel.Comments = await _product.ListOfCommentsForProductId(id);

            #endregion

            return returnModel;
        }

        //Get List Of Product Categries For Show In Site Bar
        public async Task<SiteSideBarViewModel> FillSiteSideBar()
        {
            SiteSideBarViewModel model = new SiteSideBarViewModel();

            #region List Of Product Category

            model.ListOfProductCategoriesForShowInSiteSideBar = await _product.ListOfProductCategoriesForShowSiteSideBar();

            #endregion

            return model;
        }

        //List Of Products
        public async Task<ListOfProductsViewModel> FilterProducts(ListOfProductsViewModel model)
        {
            return await _product.FilterProducts(model);
        }

        //List Of Categories For Show In List Of Product
        public async Task<List<ListOfCategoriesForShowInListOfProducts>> ListOfCategoriesForShowInListOfProducts()
        {
            return await _product.ListOfCategoriesForShowInListOfProducts();
        }

        //List Of Colors For Show In List Of Products
        public async Task<List<ListOfColorsForShowInListOfProducts>> ListOfColorsForShowInListOfProducts()
        {
            return await _product.ListOfColorsForShowInListOfProducts();
        }

        //List Of Sizes For Show In List Of Products
        public async Task<List<ListOfSizesForShowInListOfProducts>> ListOfSizesForShowInListOfProducts()
        {
            return await _product.ListOfSizesForShowInListOfProducts();
        }

        //Fill Newest 3 Products 
        public async Task<List<LastestProducts>> FillNewest3Products()
        {
            return await _product.FillNewest3Products();
        }

        //Get Maximum Prices Of Products
        public async Task<int> GetMaximumPricesOfProducts()
        {
            return await _product.GetMaximumPricesOfProducts();
        }

        //Get Minimum Prices Of Products
        public async Task<int> GetMinimumPricesOfProducts()
        {
            return await _product.GetMinimumPricesOfProducts();
        }

        //List OF User Favorite Products Ids
        public async Task<List<int>> ListOFUserFavoriteProductsIds(int userId)
        {
            return await _product.ListOFUserFavoriteProductsIds(userId);
        }

        #endregion

        #region Admin Side 

        //Fill List Of Product Colors For Choose Admin Side View Model
        public async Task<List<ListOfProductColorsForChooseAdminSideViewModel>> FillListOfProductColorsForChooseAdminSideViewModel()
        {
            return await _product.FillListOfProductColorsForChooseAdminSideViewModel();
        }

        //Fill List Of Product Size For Choose Admin Side View Model
        public async Task<List<ListOfProductSizesForChooseAdminSideViewModel>> FillListOfProductSizesForChooseAdminSideViewModel()
        {
            return await _product.FillListOfProductSizesForChooseAdminSideViewModel();
        }

        //Add Color And Size For This Product
        public async Task<bool> AddColorAndSizeForThisProduct(int productId, List<int> colorIds, List<int> sizesId)
        {
            #region Add Color 

            List<ProductSelectedColors> productColors = new List<ProductSelectedColors>();

            foreach (var color in colorIds)
            {
                ProductSelectedColors childModel = new ProductSelectedColors()
                {
                    ColorId = color,
                    ProductId = productId,
                };

                productColors.Add(childModel);
            }

            await _product.AddProductSelectedColorWithoutSaveChanges(productColors);

            #endregion

            #region Add Size 

            List<ProductSelectedSize> productSize = new List<ProductSelectedSize>();

            foreach (var sizeId in sizesId)
            {
                ProductSelectedSize sizeChildModel = new ProductSelectedSize()
                {
                    SizeId = sizeId,
                    ProductId = productId,
                };

                productSize.Add(sizeChildModel);
            }

            await _product.AddProductSelectedSizesWithoutSaveChanges(productSize);

            #endregion

            await _product.SaveChanges();

            return true;
        }

        //Get All Product Selected Size
        public async Task<List<int>> GetAllProductSelectedSize(int productId)
        {
            return await _product.GetAllProductSelectedSize(productId);
        }

        //Get All Product Selected Color
        public async Task<List<int>> GetAllProductSelectedColor(int productId)
        {
            return await _product.GetAllProductSelectedColor(productId);
        }

        //Update Product Selected Colors And Sizes
        public async Task<bool> UpdateProductSelectedColorsAndSizes(int productId, List<int> colorsId, List<int> sizesId)
        {
            #region Delete Last Records

            await _product.UpdateProductColorAndSizes(productId);

            #endregion

            #region Add New Records

            await AddColorAndSizeForThisProduct(productId , colorsId , sizesId);

            #endregion

            return true;
        }

        //Check That Is Exist Product With This Color
        public async Task<bool> CheckThatIsExistProductWithThisColor(int productId, int colorId)
        {
            return await _product.CheckThatIsExistProductWithThisColor(productId, colorId);
        }

        //Check That Is Exist Product With This Size
        public async Task<bool> CheckThatIsExistProductWithThisSize(int productId, int sizeId)
        {
            return await _product.CheckThatIsExistProductWithThisSize(productId, sizeId);
        }

        #endregion
    }
}
