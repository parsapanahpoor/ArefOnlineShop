using Domain.Models.Product;
using Domain.Models.SizeHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISizeHelperRepository
    {
        #region Admin Side 

        Task<List<SizeHelper>> GetProductSizeHelper_ByProductId(int productId);

        Task Add_SizeHelper_ToProduct_Async(SizeHelper sizeHelper);

        Task SaveChanges_Async();

        Task<SizeHelper> Get_ProductSizeHelper_ByProductId(int id);

        void Delete_ProductSizeHelper(SizeHelper sizeHelper);

        #endregion
    }
}
