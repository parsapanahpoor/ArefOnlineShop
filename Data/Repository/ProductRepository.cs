#region Using

using Data.Context;
using Domain.Interfaces;
using Domain.Models.Product;
using Domain.ViewModels.SiteSide.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        #endregion
    }
}
