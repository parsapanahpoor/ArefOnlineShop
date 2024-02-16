using Data.Context;
using Domain.Interfaces;
using Domain.Models.Product;
using Domain.Models.SizeHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SizeHelperRepository : ISizeHelperRepository
    {
        #region Ctor

        private readonly ParsaWorkShopContext _context;

        public SizeHelperRepository(ParsaWorkShopContext context)
        {
                _context = context;
        }

        #endregion

        #region Admin Side 

        public async Task<List<SizeHelper>> GetProductSizeHelper_ByProductId(int productId)
        {
            return await  _context.SizeHelper
                           .AsNoTracking()
                           .Where(p => p.ProductId == productId).ToListAsync();
        }

        public async Task Add_SizeHelper_ToProduct_Async(SizeHelper sizeHelper)
        {
            await _context.SizeHelper.AddAsync(sizeHelper);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges_Async()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<SizeHelper> Get_ProductSizeHelper_ByProductId(int id)
        {
            return await _context.SizeHelper.FindAsync(id);
        }

        public void Delete_ProductSizeHelper(SizeHelper sizeHelper)
        {
            _context.SizeHelper.Remove(sizeHelper);
        }

        #endregion
    }
}
