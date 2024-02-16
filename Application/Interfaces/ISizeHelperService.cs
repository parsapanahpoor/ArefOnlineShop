using Domain.Models.Product;
using Domain.Models.SizeHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISizeHelperService 
    {
        #region Admin Side 

        Task<List<SizeHelper>> GetProductSizeHelper_ByProductId(int productId);

        Task Add_SizeHelper_ToProduct_Async(SizeHelper sizeHelper);

        Task<SizeHelper> Get_ProductSizeHelper_ByProductId(int id);

        Task Delete_ProductSizeHelper(SizeHelper sizeHelper);

        #endregion
    }
}
