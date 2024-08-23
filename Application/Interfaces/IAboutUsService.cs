using Domain.Models.AboutUs;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Interfaces
{
    public interface IAboutUsService 
    {
        #region Admin Side 

        Task<AboutUs> GetAboutUs(CancellationToken cancellationToken);

        Task<bool> AddOrEditAboutUs(AboutUs newAboutUs, CancellationToken cancellation);

        #endregion
    }
}
