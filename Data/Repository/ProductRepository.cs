#region Using

using Data.Context;
using Domain.Interfaces;
using Domain.Models.Comment;
using Domain.Models.Order;
using Domain.Models.Product;
using Domain.Models.Users;
using Domain.ViewModels.Admin.Product;
using Domain.ViewModels.SiteSide.Home;
using Domain.ViewModels.SiteSide.Product;
using Domain.ViewModels.SiteSide.SitSideBar;
using Domain.ViewModels.UserPanel.Dashboard;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        #region Ctor

        private ParsaWorkShopContext _context;

        public ProductRepository(ParsaWorkShopContext context)
        {
            _context = context;
        }

        #endregion

        #region Old Methods

        public void AddCategoryToProduct(ProductSelectedCategory product)
        {
            _context.ProductSelectedCategory.Add(product);
            Savechanges();
        }

        public void AddFeatureToProduct(ProductFeature feature)
        {
            _context.ProductFeature.Add(feature);
            Savechanges();
        }

        public void AddProduct(Product product)
        {
            _context.product.Add(product);
            Savechanges();
        }

        public void AddProductCategories(ProductCategories productCategories)
        {
            _context.ProductCategories.Add(productCategories);
            Savechanges();
        }

        public void AddProductGallery(ProductGallery product)
        {
            _context.ProductGallery.Add(product);
            Savechanges();
        }

        //Check That Has Product a Seconde Pic
        public bool CheckThatHasProductaSecondePic(int productId)
        {
            return _context.ProductGallery
                                 .AsNoTracking()
                                 .Where(p => p.ProductID == productId && p.ShowForSecondeMainImage)
                                 .Any();
        }

        public void DeleteProductFeature(ProductFeature feature)
        {
            _context.ProductFeature.Remove(feature);
            Savechanges();
        }

        public void DeleteProductGallery(ProductGallery product)
        {
            _context.ProductGallery.Remove(product);
            Savechanges();
        }

        public void EditProductSelectedCategory(int productid)
        {
            _context.ProductSelectedCategory.Where(p => p.ProductID == productid).ToList()
                                                                .ForEach(p => _context.ProductSelectedCategory.Remove(p));
        }

        public List<Product> GetAllDeletedProducts()
        {
            IQueryable<Product> result = _context.product.Include(p => p.Users)
                                      .IgnoreQueryFilters().Where(u => u.IsDelete);

            return result.ToList();
        }

        public List<ProductCategories> GetAllProductCategories()
        {
            return _context.ProductCategories.ToList();
        }

        public IQueryable<Product> GetAllProductForIQueryAble()
        {
            return _context.product.Include(p => p.Users).Include(p => p.ProductSelectedCategory)
                                   .Where(p => p.IsActive && p.ProductCount > 0).OrderByDescending(p => p.CreateDate);
        }

        public List<Product> GetAllProducts()
        {
            return _context.product.Include(p => p.Users).OrderByDescending(p => p.CreateDate).ToList();
        }

        public List<ProductSelectedCategory> GetAllProductSelectedCategories()
        {
            return _context.ProductSelectedCategory.ToList();
        }

        public List<Product> GetAllProductsInOffer()
        {
            return _context.product.Where(p => p.OldPrice != null && p.IsInOffer == true).ToList();
        }

        public List<Product> GetAllProductsNotInOffer()
        {
            return _context.product.Include(p => p.Users).Where(p => p.IsInOffer != true && p.IsInOffer == null).ToList();
        }

        public ProductFeature GetFeatureById(int id)
        {
            return _context.ProductFeature.Find(id);
        }

        public List<ProductGallery> GetGalleryById(int id)
        {
            return _context.ProductGallery.Where(p => p.ProductID == id).ToList();
        }

        public List<Product> GetLastestOfferProducts()
        {
            return _context.product.Where(p => p.IsInOffer == true).OrderByDescending(p => p.CreateDate).Take(4).ToList();
        }

        public List<Product> GetLastestProductsIndexPageUnder4()
        {
            return _context.product.OrderByDescending(p => p.CreateDate).ToList();
        }

        public List<Product> GetLastestProductsIndexPageUnder8()
        {
            return _context.product.OrderByDescending(p => p.CreateDate).ToList();
        }

        public List<Product> GetLastestProductsIndexPageUpper4()
        {
            return _context.product.OrderByDescending(p => p.CreateDate).Take(4).ToList();
        }

        public List<Product> GetLastestProductsIndexPageUpper8()
        {
            return _context.product.OrderByDescending(p => p.CreateDate).Take(8).ToList();
        }

        public Product GetProductByID(int productid)
        {
            return _context.product.Include(p => p.Users).FirstOrDefault(p => p.ProductID == productid);
        }

        public ProductCategories GetProductCatgeoriesById(int id)
        {
            return _context.ProductCategories.Find(id);
        }

        public List<ProductFeature> GetProductFeaturs(int id)
        {
            return _context.ProductFeature.Where(p => p.ProductID == id).ToList();
        }

        public Product GetProductForShowInSingleProducPage(int id)
        {
            return _context.product.Where(p => p.ProductID == id)
                                            .Include(p => p.ProductFeatures).Include(p => p.ProductGalleries)
                                                .SingleOrDefault();
        }

        public ProductGallery GetProductGalleryByID(int id)
        {
            return _context.ProductGallery.Find(id);
        }

        public Tuple<List<Product>, int> GetProductsForShowInHomePage(int? Categroyid, int pageId = 1, string filter = "", int take = 0)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetProductsHaveThisCategory(int category)
        {
            return _context.ProductSelectedCategory.Where(p => p.ProductCategoryId == category).Include(p => p.Product)
                            .ThenInclude(p => p.Users).Select(p => p.Product);
        }

        public bool IsExistPRoduct(int productid)
        {
            return _context.product.Any(p => p.ProductID == productid);
        }
        public int ProductCount()
        {
            return _context.product.Count();
        }

        public void Savechanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<Product> SearchForProducts(string filter)
        {
            return _context.product.Where(c => c.ProductTitle.Contains(filter) || c.Tags.Contains(filter)).Include(p => p.Users);
        }

        public void UpdateProduct(Product product)
        {
            _context.product.Update(product);
            Savechanges();
        }

        public void UpdateProductCategories(ProductCategories productCategories)
        {
            _context.ProductCategories.Update(productCategories);
            Savechanges();
        }

        #endregion

        #region Site Side 

        //Get Product Title With Product Id
        public async Task<string> GetProductTitleWithProductId(int id)
        {
            return await _context.product
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.ProductID == id)
                                 .Select(p => p.ProductTitle)
                                 .FirstOrDefaultAsync();
        }

        //Fill Product Detail Site Side View Model
        public async Task<ProductDetailSiteSideViewModel> FillProductDetailSiteSideViewModel(int id)
        {
            return await _context.product
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.ProductID == id && p.IsActive)
                                 .Select(p => new ProductDetailSiteSideViewModel()
                                 {
                                     CreateDate = p.CreateDate,
                                     IsInOffer = p.IsInOffer,
                                     LongDescription = p.LongDescription,
                                     OfferPercent = p.OfferPercent,
                                     OldPrice = p.OldPrice,
                                     Price = p.Price,
                                     ProdcutId = p.ProductID,
                                     ProductCount = p.ProductCount,
                                     ProductTitle = p.ProductTitle,
                                     ProductImageName = p.ProductImageName,
                                     ShortDescription = p.ShortDescription,
                                     Tags = p.Tags,
                                     ProductFeatures = _context.ProductFeature
                                                              .AsNoTracking()
                                                              .Where(s => s.ProductID == p.ProductID)
                                                              .ToList(),
                                     ProductGallery = _context.ProductGallery
                                                              .AsNoTracking()
                                                              .Where(s => s.ProductID == p.ProductID)
                                                              .Take(5)
                                                              .ToList(),
                                 })
                                 .FirstOrDefaultAsync();
        }

        //Check That Is Exist Product With This Color
        public async Task<bool> CheckThatIsExistProductWithThisColor(int productId, int colorId)
        {
            return await _context.ProductSelectedColors
                                 .AnyAsync(p => !p.IsDelete && p.ProductId == productId && p.ColorId == colorId);
        }

        //Check That Is Exist Product With This Size
        public async Task<bool> CheckThatIsExistProductWithThisSize(int productId, int sizeId)
        {
            return await _context.ProductSelectedSizes
                                 .AnyAsync(p => !p.IsDelete && p.ProductId == productId && p.SizeId == sizeId);
        }

        //List Of Product Categories For Show Site Side Bar
        public async Task<List<ListOfProductCategoriesForShowInSiteSideBar>> ListOfProductCategoriesForShowSiteSideBar()
        {
            return await _context.ProductCategories
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete)
                                 .Select(p => new ListOfProductCategoriesForShowInSiteSideBar()
                                 {
                                     CategoryId = p.ProductCategoryId,
                                     CategoryTitle = p.CategoryTitle
                                 })
                                 .ToListAsync();
        }

        //List Of Products
        public async Task<ListOfProductsViewModel> FilterProducts(ListOfProductsViewModel model)
        {
            var query = _context.product
                .AsNoTracking()
                .Where(s => !s.IsDelete && s.IsActive)
                .OrderByDescending(s => s.CreateDate)
                .AsQueryable();

            #region State

            //switch (filter.CourseActiveStatus)
            //{
            //    case CourseActiveStatus.All:
            //        break;
            //    case CourseActiveStatus.Active:
            //        query = query.Where(s => s.IsActive);
            //        break;
            //    case CourseActiveStatus.NotActive:
            //        query = query.Where(s => !s.IsActive);
            //        break;
            //}

            #endregion

            #region Filter

            //Color
            if (model.ColorsId != null && model.ColorsId.Any())
            {
                var colorProducts = _context.ProductSelectedColors
                                .AsNoTracking()
                                .Include(p => p.Product)
                                .Where(p => !p.IsDelete && model.ColorsId.Contains(p.ColorId))
                                .Select(p => p.Product)
                                .Distinct()
                                .AsQueryable();

                query = from q in query
                        join c in colorProducts
                        on q.ProductID equals c.ProductID
                        select new Product
                        {
                            ProductID = q.ProductID,
                            CreateDate = q.CreateDate,
                            IsActive = q.IsActive,
                            IsDelete = q.IsDelete,
                            IsInOffer = q.IsInOffer,
                            OldPrice = q.OldPrice,
                            Price = q.Price,
                            ProductImageName = q.ProductImageName,
                            ProductTitle = q.ProductTitle,
                        };
            }

            //Size
            if (model.SizesId != null && model.SizesId.Any())
            {
                var sizeProducts = _context.ProductSelectedSizes
                                .Include(p => p.Product)
                                .Where(p => !p.IsDelete && model.SizesId.Contains(p.SizeId))
                                .Select(p => p.Product)
                                .Distinct()
                                .AsQueryable();

                query = from q in query
                        join s in sizeProducts
                        on q.ProductID equals s.ProductID
                        select new Product
                        {
                            ProductID = q.ProductID,
                            CreateDate = q.CreateDate,
                            IsActive = q.IsActive,
                            IsDelete = q.IsDelete,
                            IsInOffer = q.IsInOffer,
                            OldPrice = q.OldPrice,
                            Price = q.Price,
                            ProductImageName = q.ProductImageName,
                            ProductTitle = q.ProductTitle,
                        };
            }

            //Size
            if (model.CategoriesId != null && model.CategoriesId.Any())
            {
                var categoryProducts = _context.ProductSelectedCategory
                                .Include(p => p.Product)
                                .Where(p => model.CategoriesId.Contains(p.ProductCategoryId))
                                .Select(p => p.Product)
                                .Distinct()
                                .AsQueryable();

                query = from q in query
                        join c in categoryProducts
                        on q.ProductID equals c.ProductID
                        select new Product
                        {
                            ProductID = q.ProductID,
                            CreateDate = q.CreateDate,
                            IsActive = q.IsActive,
                            IsDelete = q.IsDelete,
                            IsInOffer = q.IsInOffer,
                            OldPrice = q.OldPrice,
                            Price = q.Price,
                            ProductImageName = q.ProductImageName,
                            ProductTitle = q.ProductTitle,
                        };
            }

            //Title
            if (!string.IsNullOrEmpty(model.ProductTitle))
            {
                query = query.Where(p => p.ProductTitle.Contains(model.ProductTitle));
            }

            #endregion

            #region Price

            if (model.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= model.MinPrice.Value);
            }

            if (model.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= model.MaxPrice.Value);
            }

            #endregion

            await model.Paging(query.Distinct());

            #region Filter Status

            switch (model.Status)
            {
                case FilterStatus.All:
                    break;
                case FilterStatus.CreateDate:
                    model.Entities = model.Entities.OrderByDescending(p => p.CreateDate).ToList();
                    break;
                case FilterStatus.MinPrice:
                    model.Entities = model.Entities.OrderBy(p => p.Price).ToList();
                    break;
                case FilterStatus.MaxPrice:
                    model.Entities = model.Entities.OrderByDescending(p => p.Price).ToList();
                    break;
                case FilterStatus.Offer:
                    model.Entities = model.Entities.Where(p => p.OldPrice.HasValue).ToList();
                    break;
            }

            #endregion

            return model;
        }

        //List Of Categories For Show In List Of Product
        public async Task<List<ListOfCategoriesForShowInListOfProducts>> ListOfCategoriesForShowInListOfProducts()
        {
            return await _context.ProductCategories
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete)
                                 .OrderBy(p => p.Priority)
                                 .Select(p => new ListOfCategoriesForShowInListOfProducts()
                                 {
                                     CategoryId = p.ProductCategoryId,
                                     CategoryTitle = p.CategoryTitle
                                 })
                                 .ToListAsync();
        }

        //List Of Colors For Show In List Of Products
        public async Task<List<ListOfColorsForShowInListOfProducts>> ListOfColorsForShowInListOfProducts()
        {
            return await _context.ProductColors
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete)
                                 .Select(p => new ListOfColorsForShowInListOfProducts()
                                 {
                                     ColorId = p.Id,
                                     ColorTitle = p.ColorTitle
                                 })
                                 .ToListAsync();
        }

        //List Of Sizes For Show In List Of Products
        public async Task<List<ListOfSizesForShowInListOfProducts>> ListOfSizesForShowInListOfProducts()
        {
            return await _context.ProductsSizes
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete)
                                 .Select(p => new ListOfSizesForShowInListOfProducts()
                                 {
                                     SizesId = p.Id,
                                     SizesTitle = p.SizeTitle
                                 })
                                 .ToListAsync();
        }

        //Fill Product Category Link able
        public async Task<List<ProductCategoryLinkable>> FillProductCategoryLinkable(int productId)
        {
            #region Get Product Categories

            var productSelectedCategories = await _context.ProductSelectedCategory
                                                          .AsNoTracking()
                                                          .Where(p => p.ProductID == productId)
                                                          .Select(p => p.ProductCategoryId)
                                                          .ToListAsync();

            #endregion

            #region Initial Return Model

            List<ProductCategoryLinkable> model = new List<ProductCategoryLinkable>();

            if (productSelectedCategories != null && productSelectedCategories.Any())
            {
                foreach (var categoryId in productSelectedCategories)
                {
                    ProductCategoryLinkable childModel = new ProductCategoryLinkable();

                    childModel = await _context.ProductCategories
                                               .AsNoTracking()
                                               .Where(p => !p.IsDelete && p.ProductCategoryId == categoryId)
                                               .Select(p => new ProductCategoryLinkable()
                                               {
                                                   CategoryId = categoryId,
                                                   CategoryTitle = p.CategoryTitle
                                               })
                                               .FirstOrDefaultAsync();

                    if (childModel != null) model.Add(childModel);
                }
            }

            #endregion

            return model;
        }

        //Fill Newest 3 Products 
        public async Task<List<LastestProducts>> FillNewest3Products(int? userId)
        {
            return await _context.product
                                 .Include(p => p.ProductGalleries)
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete)
                                 .Select(p => new LastestProducts()
                                 {
                                     IsInOffer = p.IsInOffer,
                                     OldPrice = p.OldPrice,
                                     Price = p.Price,
                                     ProductId = p.ProductID,
                                     ProductImageName = p.ProductImageName,
                                     Title = p.ProductTitle,
                                     SecondeProductImageName = _context.ProductGallery
                                                                       .AsNoTracking()
                                                                       .Where(s => s.ProductID == p.ProductID)
                                                                       .Select(s => s.ImageName)
                                                                       .FirstOrDefault(),
                                     IsInFavorite = !userId.HasValue ?
                                                         false
                                                         :
                                                         _context.FavoriteProducts.Any(s => !s.IsDelete && s.UserId == userId.Value && s.ProductId == p.ProductID)

                                 })
                                 .Take(3)
                                 .ToListAsync();
        }

        //Get Maximum Prices Of Products
        public async Task<int> GetMaximumPricesOfProducts()
        {
            return await _context.product
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.IsActive)
                                 .OrderByDescending(p => p.Price)
                                 .Select(p => Convert.ToInt32(p.Price))
                                 .FirstOrDefaultAsync();
        }

        //Get Minimum Prices Of Products
        public async Task<int> GetMinimumPricesOfProducts()
        {
            return await _context.product
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.IsActive)
                                 .OrderBy(p => p.Price)
                                 .Select(p => Convert.ToInt32(p.Price))
                                 .FirstOrDefaultAsync();
        }

        //List OF User Favorite Products Ids
        public async Task<List<int>> ListOFUserFavoriteProductsIds(int userId)
        {
            return await _context.FavoriteProducts
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.UserId == userId)
                                 .Select(p => p.ProductId)
                                 .ToListAsync();
        }

        //List Of Comments For ProductId
        public async Task<List<Comment>> ListOfCommentsForProductId(int productId)
        {
            return await _context.Comment
                                 .Include(p => p.Users)
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.ProductTypeId == 1 && p.ProductID == productId)
                                 .ToListAsync();
        }

        //Get Product Name By Product Id
        public async Task<string> GetProductNameByProductId(int productId)
        {
            return await _context.product
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.ProductID == productId)
                                 .Select(p => p.ProductTitle)
                                 .FirstOrDefaultAsync();
        }

        #endregion

        #region Admin Side 

        //Fill List Of Product Colors For Choose Admin Side View Model
        public async Task<List<ListOfProductColorsForChooseAdminSideViewModel>> FillListOfProductColorsForChooseAdminSideViewModel()
        {
            return await _context.ProductColors
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete)
                                 .Select(p => new ListOfProductColorsForChooseAdminSideViewModel()
                                 {
                                     ColorId = p.Id,
                                     ColorTitle = p.ColorTitle
                                 })
                                 .ToListAsync();
        }

        //Fill List Of Product Size For Choose Admin Side View Model
        public async Task<List<ListOfProductSizesForChooseAdminSideViewModel>> FillListOfProductSizesForChooseAdminSideViewModel()
        {
            return await _context.ProductsSizes
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete)
                                 .Select(p => new ListOfProductSizesForChooseAdminSideViewModel()
                                 {
                                     SizeId = p.Id,
                                     SizeTitle = p.SizeTitle
                                 })
                                 .ToListAsync();
        }

        //Add Product Selected Sizes Without Save Changes
        public async Task AddProductSelectedSizesWithoutSaveChanges(List<ProductSelectedSize> selectedSize)
        {
            await _context.ProductSelectedSizes.AddRangeAsync(selectedSize);
        }

        //Add Product Selected Color Without Save Changes
        public async Task AddProductSelectedColorWithoutSaveChanges(List<ProductSelectedColors> selectedColors)
        {
            await _context.ProductSelectedColors.AddRangeAsync(selectedColors);
        }

        //Save Changes
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        //Get All Product Selected Size
        public async Task<List<int>> GetAllProductSelectedSize(int productId)
        {
            return await _context.ProductSelectedSizes
                                  .Where(p => !p.IsDelete && p.ProductId == productId)
                                  .Select(p => p.SizeId)
                                  .ToListAsync();
        }

        //Get All Product Selected Color
        public async Task<List<int>> GetAllProductSelectedColor(int productId)
        {
            return await _context.ProductSelectedColors
                                  .Where(p => !p.IsDelete && p.ProductId == productId)
                                  .Select(p => p.ColorId)
                                  .ToListAsync();
        }

        //Update Product Color And Sizes
        public async Task UpdateProductColorAndSizes(int productId)
        {
            #region Update Colors

            //Remove Last Datas
            var lastColorRecords = _context.ProductSelectedColors
                                           .Where(p => !p.IsDelete && p.ProductId == productId)
                                           .ToList();

            if (lastColorRecords != null && lastColorRecords.Any()) _context.ProductSelectedColors.RemoveRange(lastColorRecords);

            #endregion

            #region Update Sizes

            //Remove Last Datas
            var lastSizesRecords = _context.ProductSelectedSizes
                                           .Where(p => !p.IsDelete && p.ProductId == productId)
                                           .ToList();

            if (lastSizesRecords != null && lastSizesRecords.Any()) _context.ProductSelectedSizes.RemoveRange(lastSizesRecords);

            #endregion

            await SaveChanges();
        }

        //Get Product Selected Color By Product Id
        public async Task<List<ProductColor>> GetProductSelectedColorByProductId(int productId)
        {
            #region Get Selected Color

            var colorsId = await _context.ProductSelectedColors
                                          .AsNoTracking()
                                          .Where(p => !p.IsDelete && p.ProductId == productId)
                                          .Select(p => p.ColorId)
                                          .ToListAsync();

            #endregion

            //Get Colors
            List<ProductColor> returnModel = new List<ProductColor>();

            if (colorsId != null && colorsId.Any())
            {
                foreach (var colorId in colorsId)
                {
                    ProductColor childModel = new ProductColor();

                    childModel = await _context.ProductColors
                                                .FirstOrDefaultAsync(p => !p.IsDelete && p.Id == colorId);

                    returnModel.Add(childModel);
                }
            }

            return returnModel;
        }

        //Get Product Selected Size By Product Id
        public async Task<List<ProductsSize>> GetProductSelectedSizeByProductId(int productId)
        {
            #region Get Selected Size

            var sizesId = await _context.ProductSelectedSizes
                                          .AsNoTracking()
                                          .Where(p => !p.IsDelete && p.ProductId == productId)
                                          .Select(p => p.SizeId)
                                          .ToListAsync();

            #endregion

            //Get Sizes
            List<ProductsSize> returnModel = new List<ProductsSize>();

            if (sizesId != null && sizesId.Any())
            {
                foreach (var sizeId in sizesId)
                {
                    ProductsSize childModel = new ProductsSize();

                    childModel = await _context.ProductsSizes
                                                .FirstOrDefaultAsync(p => !p.IsDelete && p.Id == sizeId);

                    returnModel.Add(childModel);
                }
            }

            return returnModel;
        }

        //User Panel Dashboard View Model
        public async Task<UserPanelDashboardViewModel> UserPanelDashboardViewModel(int userId)
        {
            UserPanelDashboardViewModel model = new UserPanelDashboardViewModel();

            #region Get User By Id

            model.User = await _context.Users
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(p => !p.IsDelete && p.UserId == userId);

            #endregion

            #region Last Order

            int? lastorder = await _context.Orders
                                             .AsNoTracking()
                                             .Where(p => p.Userid == userId)
                                             .OrderByDescending(p => p.CreateDate)
                                             .Select(p => p.OrderId)
                                             .FirstOrDefaultAsync();

            if (lastorder.HasValue)
            {
                var proIds = await _context.OrderDetails
                                                       .AsNoTracking()
                                                       .Where(p => p.OrderID == lastorder)
                                                       .Select(p => p.ProductID)
                                                       .Take(3)
                                                       .ToListAsync();

                if (proIds is not null)
                {
                    List<Product> products = new List<Product>();

                    foreach (var productId in proIds)
                    {
                        var product = await _context.product
                                                    .AsNoTracking()
                                                    .Include(p=> p.ProductGalleries)
                                                    .FirstOrDefaultAsync(p => !p.IsDelete && p.ProductID == productId);
                        if (product != null) products.Add(product);
                    }

                    model.LastOrder = products;
                }
            }

            #endregion

            #region Last Favorite

            var favoriteProds = await _context.FavoriteProducts
                                              .AsNoTracking()
                                              .Where(p => !p.IsDelete && p.UserId == userId)
                                              .OrderByDescending(p => p.CreateDate)
                                              .Select(p => p.ProductId)
                                              .Take(3)
                                              .ToListAsync();

            List<Product> favoirteProducts = new List<Product>();

            if (favoriteProds != null && favoriteProds.Any())
            {
                foreach (var prodsId in favoriteProds)
                {
                    Product product = await _context.product
                                                   .AsNoTracking()
                                                   .Include(p=>p.ProductGalleries)
                                                   .FirstOrDefaultAsync(p => !p.IsDelete && p.ProductID == prodsId);

                    if (product != null) favoirteProducts.Add(product);
                }
            }

            model.LastFavoriteProduct = favoirteProducts;

            #endregion

            return model;
        }

        #endregion
    }
}
