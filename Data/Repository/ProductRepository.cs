#region Using

using Data.Context;
using Domain.Interfaces;
using Domain.Models.Product;
using Domain.ViewModels.Admin.Product;
using Domain.ViewModels.SiteSide.Product;
using Domain.ViewModels.SiteSide.SitSideBar;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
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
            return _context.product.Include(p => p.Users).ToList();
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
            return _context.product.Where(p => p.IsInOffer != true && p.IsInOffer == null).ToList();
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

        //Fill Product Detail Site Side View Model
        public async Task<ProductDetailSiteSideViewModel> FillProductDetailSiteSideViewModel(int id)
        {
            return await _context.product
                                 .AsNoTracking()
                                 .Where(p => !p.IsDelete && p.ProductID == id)
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
                                                              .ToList(),
                                 })
                                 .FirstOrDefaultAsync();
        }

        //Check That Is Exist Product With This Color
        public async Task<bool> CheckThatIsExistProductWithThisColor(int productId , int colorId)
        {
            return await _context.ProductSelectedColors
                                 .AnyAsync(p=> !p.IsDelete && p.ProductId == productId && p.ColorId == colorId);
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
                                 .Where(p=> !p.IsDelete)
                                 .Select(p=> new ListOfProductCategoriesForShowInSiteSideBar()
                                 {
                                     CategoryId = p.ProductCategoryId,
                                     CategoryTitle = p.CategoryTitle
                                 })
                                 .ToListAsync();
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

            if(lastColorRecords != null && lastColorRecords.Any()) _context.ProductSelectedColors.RemoveRange(lastColorRecords);

            #endregion

            #region Update Sizes

            //Remove Last Datas
            var lastSizesRecords = _context.ProductSelectedSizes
                                           .Where(p => !p.IsDelete && p.ProductId == productId)
                                           .ToList();

            if (lastSizesRecords != null && lastSizesRecords.Any())  _context.ProductSelectedSizes.RemoveRange(lastSizesRecords);

            #endregion

            await SaveChanges();
        }

        //Get Product Selected Color By Product Id
        public async Task<List<ProductColor>> GetProductSelectedColorByProductId(int productId)
        {
            #region Get Selected Color

            var colorsId  = await _context.ProductSelectedColors
                                          .AsNoTracking()
                                          .Where(p=> !p.IsDelete && p.ProductId == productId)
                                          .Select(p=> p.ColorId)
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

        #endregion
    }
}
