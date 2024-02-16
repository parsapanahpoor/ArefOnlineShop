using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models.Product;
using Domain.Models.SizeHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SizeHelperService : ISizeHelperService
    {
        #region Ctor

        private readonly ISizeHelperRepository _sizeHelperRepository;

        public SizeHelperService(ISizeHelperRepository sizeHelperRepository)
        {
            _sizeHelperRepository = sizeHelperRepository;
        }

        #endregion

        #region Admin Side 

        public async Task<List<SizeHelper>> GetProductSizeHelper_ByProductId(int productId)
        {
            return await _sizeHelperRepository.GetProductSizeHelper_ByProductId(productId);
        }

        public async Task Add_SizeHelper_ToProduct_Async(SizeHelper sizeHelper)
        {
            await _sizeHelperRepository.Add_SizeHelper_ToProduct_Async(sizeHelper);
        }

        public async Task<SizeHelper> Get_ProductSizeHelper_ByProductId(int id)
        {
            return await Get_ProductSizeHelper_ByProductId(id);
        }

        public async Task Delete_ProductSizeHelper(SizeHelper sizeHelper)
        {
            _sizeHelperRepository.Delete_ProductSizeHelper(sizeHelper);
            await _sizeHelperRepository.SaveChanges_Async();
        }

        #endregion
    }
}
